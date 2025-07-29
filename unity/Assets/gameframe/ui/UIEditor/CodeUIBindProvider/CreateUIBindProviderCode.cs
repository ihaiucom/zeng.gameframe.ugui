#if UNITY_EDITOR
using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Zeng.GameFrame.UIS.Editor
{
    public class CreateUIBindProviderCode : BaseTemplate
    {
        public override string EventName => "UI反射动态码";

        public override bool Cover => true;

        public override bool AutoRefresh => true;

        public override bool ShowTips => false;

        public CreateUIBindProviderCode(out bool result, string authorName, UIBindProviderData codeData) : base(
            authorName)
        {
            #if UI_ET
            var path     = $"{UISettingConfigEditor.Instance.UICodeScriptsPath}/{codeData.Name}.cs";
            #else
            var path     = $"{UISettingConfigEditor.Instance.UIGenerationPath}/{codeData.Name}.cs";
            #endif
            
            var template = $"{UISettingConfigEditor.Instance.UITemplatePath}/UIBindProviderTemplate.txt";
            CreateVo = new CreateVo(template, path);

            ValueDic["Count"]   = codeData.Count.ToString();
            ValueDic["Content"] = codeData.Content;

            result = CreateNewFile();
        }
    }
}
#endif