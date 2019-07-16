﻿using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace OpenRPA.Office.Activities
{
    public class outlookfolder
    {
        public string name { get; set; }
        public string _id { get; set; }
    }
    // Interaction logic for addinputDesigner.xaml
    public partial class GetMailsDesigner //: INotifyPropertyChanged
    {
        public ObservableCollection<outlookfolder> folders { get; set; }
        public GetMailsDesigner()
        {
            folders = new ObservableCollection<outlookfolder>();
            folders.Add(new outlookfolder() { name = "", _id = "" });
            InitializeComponent();
        }

        private void ActivityDesigner_Loaded(object sender, RoutedEventArgs e)
        {
            Microsoft.Office.Interop.Outlook.Application outlookApplication = new Microsoft.Office.Interop.Outlook.Application();
            MAPIFolder inBox = (MAPIFolder)outlookApplication.ActiveExplorer().Session.GetDefaultFolder(OlDefaultFolders.olFolderInbox);
            MAPIFolder folderbase = inBox.Store.GetRootFolder();
            foreach (MAPIFolder folder in folderbase.Folders)
            {
                GetFolders(folder, 0);
            }
        }
        public void GetFolders(MAPIFolder folder, int ident)
        {
            if (folder.Folders.Count == 0)
            {
                //if (folder.Name == "Folder Name")
                //{
                //    mailsFromThisFolder = folder;
                //}
                folders.Add(new outlookfolder() { name = space(ident * 5) + folder.Name, _id = folder.FullFolderPath });
            }
            else
            {
                foreach (MAPIFolder subFolder in folder.Folders)
                {
                    folders.Add(new outlookfolder() { name = space(ident * 5) + folder.Name, _id = folder.FullFolderPath });
                    GetFolders(subFolder, (ident + 1));
                }
            }
        }
        public string space(int num)
        {
            return new String(' ', num);
        }

        //public event PropertyChangedEventHandler PropertyChanged;
        //private void NotifyPropertyChanged(String propertyName)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

        //private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    NotifyPropertyChanged("theCommentOnLiner");
        //}
    }
}