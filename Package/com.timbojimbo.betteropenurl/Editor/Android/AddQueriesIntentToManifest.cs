#if UNITY_ANDROID

using UnityEditor.Android;

namespace TimboJimbo.BetterOpenURL.Editor.Android
{
    public class AddQueriesIntentToManifest : IPostGenerateGradleAndroidProject
    {

        public int callbackOrder => 0;

        public void OnPostGenerateGradleAndroidProject(string path)
        {
            const string AndroidXmlNamespace = "http://schemas.android.com/apk/res/android";

            // We need to add the following to detect if the CustomTabsService is available on the device, so we 
            // know when we need to fallback to Application.OpenURL...!
            // <manifest>
            //    <queries>
            //        <intent>
            //            <action android:name="android.support.customtabs.action.CustomTabsService" />
            //        </intent>
            //    </queries>
            // </manifest>

            string manifestPath = path + "/src/main/AndroidManifest.xml";

            var doc = new System.Xml.XmlDocument();
            doc.Load(manifestPath);

            var manifest = doc.SelectSingleNode("/manifest");
        
            var queries = doc.CreateElement("queries");
            var intent = doc.CreateElement("intent");
            var action = doc.CreateElement("action");
            var actionAttribute = doc.CreateAttribute("android", "name", AndroidXmlNamespace);
            actionAttribute.Value = "android.support.customtabs.action.CustomTabsService";
            action.Attributes?.Append(actionAttribute);
            intent.AppendChild(action);
            queries.AppendChild(intent);
            manifest.AppendChild(queries);

            doc.Save(manifestPath);
        }
    }
}
#endif