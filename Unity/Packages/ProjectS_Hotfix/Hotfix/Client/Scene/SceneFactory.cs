namespace ET.Client
{
    public static class SceneFactory
    {
        public static Scene CreateClientScene(int zone, string name, Entity parent)
        {
            Scene clientScene = EntitySceneFactory.CreateScene(zone, SceneType.Client, name, parent);
            clientScene.AddComponent<ClientSceneFlagComponent>();
            clientScene.AddComponent<CurrentScenesComponent>();
            clientScene.AddComponent<PlayerComponent>();
            clientScene.AddComponent<YooAssetComponent>();
            
            Game.EventSystem.Publish(clientScene, new EventType.AfterCreateClientScene());
            return clientScene;
        }
        
        /// <summary>
        /// 创建单局游戏Scene，并设置当前单局游戏Scene为此新创建的Scene，并且Dispose之前的
        /// </summary>
        /// <param name="zone"></param>
        /// <param name="name"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static Scene CreateSingleGameScene(int zone, string name, Entity parent)
        {
            Scene singleGameScene = EntitySceneFactory.CreateScene(zone, SceneType.SingleGame, name, parent);
            
            singleGameScene.AddComponent<ClientSceneFlagComponent>();

            ClientSceneManagerComponent.Instance.SetCurrentSingleGameScene(singleGameScene);
            
            Game.EventSystem.Publish(singleGameScene, new EventType.AfterCreateSingleGameScene());
            
            return singleGameScene;
        }
        
        public static Scene CreateCurrentScene(long id, int zone, string name, CurrentScenesComponent currentScenesComponent)
        {
            Scene currentScene = EntitySceneFactory.CreateScene(id, IdGenerater.Instance.GenerateInstanceId(), zone, SceneType.Current, name, currentScenesComponent);
            currentScenesComponent.Scene = currentScene;
            
            Game.EventSystem.Publish(currentScene, new EventType.AfterCreateCurrentScene());
            return currentScene;
        }
        
        
    }
}