using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ThemModdingHerds.ZEngineUI.Base;
using ThemModdingHerds.ZEngineUI.Data;

namespace ZEngineUIEditor
{
    internal class ControlDataItem : TreeViewItem
    {
        public ControlData ControlData { get; set; }
        public ControlDataItem(ControlData data)
        {
            Header = data.Name;
            ControlData = data;
            DataContext = data;
        }
        public static ControlDataItem CreateViewItem(ControlData data)
        {
            ControlDataItem treeViewItem = new(data);
            if (data is GroupData groupData)
            {
                IEnumerable<ControlDataItem> items = from c in groupData.Controls select CreateViewItem(c);
                foreach (ControlDataItem item in items)
                {
                    treeViewItem.Items.Add(item);
                }
            }
            if (data is ViewData viewData)
            {
                IEnumerable<ControlDataItem> items = from c in viewData.Controls select CreateViewItem(c);
                foreach (ControlDataItem item in items)
                {
                    treeViewItem.Items.Add(item);
                }
            }

            return treeViewItem;
        }
        public static TreeView CreatePropertiesTree(ControlData data)
        {
            TreeView item = new();

            Type type = data.GetType();
            Type? derived = type;
            do
            {
                derived = derived.BaseType;
                if(derived != null)
                {
                    TreeViewItem derivedItem = new()
                    {
                        Header = derived.Name
                    };
                    PropertyInfo[] properties = derived.GetProperties();
                    foreach(PropertyInfo propertyInfo in properties)
                    {
                        TreeViewItem propItem = new()
                        {
                            Header = propertyInfo.Name,
                        };
                        derivedItem.Items.Add(propItem);
                    }
                    item.Items.Add(derivedItem);
                }
            } while (derived != null);

            return item;
        }
    }
}
