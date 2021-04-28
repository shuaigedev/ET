using ET;
using UnityEngine;

namespace ETHotfix
{
    public class AI_XunLuo: AAIHandler
    {
        public override int Check(AIComponent aiComponent, AIConfig aiConfig)
        {
            long sec = TimeHelper.ClientNow() / 1000 % 15;
            if (sec < 10)
            {
                return 0;
            }
            return 1;
        }

        public override async ETVoid Execute(AIComponent aiComponent, AIConfig aiConfig, ETCancellationToken cancellationToken)
        {
            Scene zoneScene = aiComponent.DomainScene();

            Unit myUnit = zoneScene.GetComponent<UnitComponent>().MyUnit;
            if (myUnit == null)
            {
                return;
            }
            
            Log.Debug("开始巡逻");

            while (true)
            {
                XunLuoPathComponent xunLuoPathComponent = myUnit.GetComponent<XunLuoPathComponent>();
                Vector3 nextTarget = xunLuoPathComponent.GetCurrent();
                int ret = await myUnit.MoveToAsync(nextTarget, cancellationToken);
                if (ret != 0)
                {
                    return;
                }
                xunLuoPathComponent.MoveNext();
            }
        }
    }
}