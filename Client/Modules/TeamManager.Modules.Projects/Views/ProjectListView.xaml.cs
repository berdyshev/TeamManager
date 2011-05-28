﻿using System;
using System.Collections.Generic;
using System.ServiceModel.DomainServices.Client;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using TeamManager.Modules.Projects.ViewModels;
using System.ComponentModel;
using TeamManager.Web.Models;

namespace TeamManager.Modules.Projects.Views
{
//    [ViewSortHint("100")]
    public partial class ProjectListView : UserControl
    {
        public ProjectListView()
        {
            DataContext = new
                              {
                                  HeaderTitle = "Projects",
                                  Projects = new List<Project>
                                                 {
                                                     new Project
                                                         {
                                                             Title = "Project title",
                                                             Description =
                                                                 "Design time data. Project description text goes here.",
                                                             CreatedOn = DateTime.Now
                                                         }
                                                 }
                              };
            InitializeComponent();
        }

        [Dependency]
        public ProjectListViewModel ViewModel { set { DataContext = value;  } }
    }
}
