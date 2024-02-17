/*
日期：
功能：资源加载用该类统一加载,后续完善  为加载ab包、编辑器测试、引入对象池准备
作者：小人
版本号：
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResLoader : MonoSingleton<ResLoader>
{
    
    //Resouces的封装 后续有需求继续封装
    public static Object ResLoad(string path)
    {
       return Resources.Load(path);
    }

    public static T ResLoad<T>(string path) where T:Object
    {
        return Resources.Load<T>(path);
    }

    //获取Resources 文件夹下的实例物体
    public static GameObject ResGetInstance(string path)
    {
      return  Instantiate(ResLoad(path)) as GameObject;
    }

    public static T ResGetInstance<T>(string path) where T:Object
    {
      GameObject g=  Instantiate(ResLoad<T>(path)) as GameObject;
       return g as T; 
    }

    //AB包的加载
    
    private string ReadPath
    {
        get
        {
#if UNITY_EDITOR

            return Application.streamingAssetsPath;
#elif UNITY_STANDALONE_WIN
             return Application.persistentDataPath;
#endif

        }
    }
    private string BagName
    {
        get
        {
#if UNITY_EDITOR ||UNITY_STANDALONE_WIN
            return "StandaloneWindows";
#endif
        }
    }

    private AssetBundle mainAB = null;
    private AssetBundleManifest manifest = null;

    private Dictionary<string,AssetBundle> abCache = new Dictionary<string,AssetBundle>();
    public AssetBundle LoadSimpleBag(string bagName)
    {
        string path = ReadPath + "/" + BagName + "/" + bagName;
     
        return AssetBundle.LoadFromFile(path);
    }

    //加载主包及其AssetBundleManifest
    private void LoadMainManifest()
    {

        mainAB = LoadSimpleBag(BagName);
        manifest = mainAB.LoadAsset("AssetBundleManifest", typeof(AssetBundleManifest))
            as AssetBundleManifest;
    }

    //加载ab包
    public AssetBundle LoadAssetBundle(string bagName)
    {
        if(manifest==null)
        {
            LoadMainManifest();
        }

      string[] res=  manifest.GetAllDependencies(bagName);
        //如果有依赖的包 先加载依赖的包
        if (res.Length > 0)
        {
            for (int i = 0; i < res.Length; i++)
            {
                //从缓存中查找是否有同名的包 
                //没有就要加载到缓存
                if (!abCache.ContainsKey(res[i]))
                {
                    abCache.Add(res[i], LoadSimpleBag(res[i]));
                }
            }
        }
        //3.加载自身
        //看缓存中是否有 有返回 无需要手动加载
        if (abCache.ContainsKey(bagName)) return abCache[bagName];
        AssetBundle ab = LoadSimpleBag(bagName);
        abCache.Add(bagName, ab);
        return ab;
    }


    //卸载ab包
    public void UnLoadAB(string bagName)
    {
        if(abCache.ContainsKey(bagName))
        {
            abCache[bagName].Unload(false);
            abCache.Remove(bagName);
        }
    }

    //卸载所有ab包
    public void UnLoadAllComplete()
    {
        AssetBundle.UnloadAllAssetBundles(true);
        mainAB = null;
        manifest = null;
        abCache.Clear();
    }


    //加载ab包中资源 
    public Object ABLoad(string abName, string gameObjName)
    {
        AssetBundle ab = LoadAssetBundle(abName);

          return ab.LoadAsset(gameObjName);
    }

    public T ABLoad<T> (string abName,string gameObjName) where T:Object
    {
        AssetBundle ab = LoadAssetBundle(abName);
        return  ab.LoadAsset(gameObjName) as T;
    }

    //根据包名 物体名 生成游戏物体实例
    public  GameObject ABGetInstance(string abName,string gameObjName)
    {
        return Instantiate(ABLoad(abName,gameObjName)) as GameObject;
    }

    public T ABGetInstance<T>(string abName, string gameObjName) where T:Object
    {
      return  ABGetInstance(abName, gameObjName) as T;
    }
}
