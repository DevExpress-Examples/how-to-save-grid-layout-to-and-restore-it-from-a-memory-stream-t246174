using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO;

namespace DXGrid_GridLayout {
    public partial class MainPage : UserControl {
        public static readonly DependencyProperty IsLayoutSavedProperty =
            DependencyProperty.Register("IsLayoutSaved", typeof(bool), typeof(MainPage), null);
        public bool IsLayoutSaved {
            get { return (bool)GetValue(IsLayoutSavedProperty); }
            set { SetValue(IsLayoutSavedProperty, value); }
        }
        MemoryStream layoutStream;
        public MainPage() {
            DataContext = this;
            InitializeComponent();
            IsLayoutSaved = false;
            grid.ItemsSource = IssueList.GetData();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e) {
            layoutStream = new MemoryStream();
            grid.SaveLayoutToStream(layoutStream);
            IsLayoutSaved = true;
        }
        private void LoadButton_Click(object sender, RoutedEventArgs e) {
            layoutStream.Position = 0;
            grid.RestoreLayoutFromStream(layoutStream);
        }
        private void Button_Click(object sender, RoutedEventArgs e) {
            grid.Columns.Add(new DevExpress.Xpf.Grid.GridColumn() { FieldName = "IsPrivate" });
        }
    }
    public class IssueList {
        static public List<IssueDataObject> GetData() {
            List<IssueDataObject> data = new List<IssueDataObject>();
            data.Add(new IssueDataObject() {
                IssueName = "Transaction History",
                IssueType = "Bug", IsPrivate = true
            });
            data.Add(new IssueDataObject() {
                IssueName = "Ledger: Inconsistency",
                IssueType = "Bug", IsPrivate = false
            });
            data.Add(new IssueDataObject() {
                IssueName = "Data Import",
                IssueType = "Request", IsPrivate = false
            });
            data.Add(new IssueDataObject() {
                IssueName = "Data Archiving",
                IssueType = "Request", IsPrivate = true
            });
            return data;
        }
    }

    public class IssueDataObject {
        public string IssueName { get; set; }
        public string IssueType { get; set; }
        public bool IsPrivate { get; set; }
    }
}
