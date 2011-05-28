﻿
namespace TeamManager.Web.Services
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.Linq;
    using System.ServiceModel.DomainServices.EntityFramework;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;
    using TeamManager.Web.Models;


    // Implements application logic using the TeamManagerDBEntities context.
    // TODO: Add your application logic to these methods or in additional methods.
    // TODO: Wire up authentication (Windows/ASP.NET Forms) and uncomment the following to disable anonymous access
    // Also consider adding roles to restrict access as appropriate.
    // [RequiresAuthentication]
    [EnableClientAccess()]
    public class TeamManagerDomainService : LinqToEntitiesDomainService<TeamManagerDBEntities>
    {

        public IQueryable<Dictionary> GetDictionaryByName(string dicName)
        {
            return ObjectContext.Dictionaries.Where(dicItem => dicItem.Type == dicName).OrderBy(dicItem => dicItem.Weight);
        }

        public IQueryable<Dictionary> GetPriorities()
        {
            return GetDictionaryByName("issue_priority");
        }

        public IQueryable<Dictionary> GetActivities()
        {
            return GetDictionaryByName("time_activity");
        }

        public void InsertDictionary(Dictionary dictionary)
        {
            if ((dictionary.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(dictionary, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Dictionaries.AddObject(dictionary);
            }
        }

        public void UpdateDictionary(Dictionary currentDictionary)
        {
            this.ObjectContext.Dictionaries.AttachAsModified(currentDictionary, this.ChangeSet.GetOriginal(currentDictionary));
        }

        public void DeleteDictionary(Dictionary dictionary)
        {
            if ((dictionary.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(dictionary, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.Dictionaries.Attach(dictionary);
                this.ObjectContext.Dictionaries.DeleteObject(dictionary);
            }
        }

        public IQueryable<Issue> GetIssues()
        {
            return this.ObjectContext.Issues.OrderByDescending(issue => issue.CreatedOn);
        }

        public IQueryable<Issue> GetIssuesByProject(int projectId)
        {
            return ObjectContext.Issues.Where(issue => issue.ProjectId == projectId)
                .OrderByDescending(issue => issue.CreatedOn);
        }

        public void InsertIssue(Issue issue)
        {
            issue.CreatedOn = DateTime.Now;
            issue.AuthorId = Guid.Parse("449e73eb-2911-4302-abae-44c0b91d8d0c");
            issue.AssignedTo = Guid.Parse("449e73eb-2911-4302-abae-44c0b91d8d0c");
            if ((issue.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(issue, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Issues.AddObject(issue);
            }
        }

        public void UpdateIssue(Issue currentIssue)
        {
            currentIssue.UpdatedOn = DateTime.Now;
            this.ObjectContext.Issues.AttachAsModified(currentIssue, this.ChangeSet.GetOriginal(currentIssue));
        }

        public void DeleteIssue(Issue issue)
        {
            if ((issue.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(issue, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.Issues.Attach(issue);
                this.ObjectContext.Issues.DeleteObject(issue);
            }
        }

        public IQueryable<IssueStatus> GetIssueStatuses()
        {
            return this.ObjectContext.IssueStatuses;
        }

        public void InsertIssueStatus(IssueStatus issueStatus)
        {
            if ((issueStatus.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(issueStatus, EntityState.Added);
            }
            else
            {
                this.ObjectContext.IssueStatuses.AddObject(issueStatus);
            }
        }

        public void UpdateIssueStatus(IssueStatus currentIssueStatus)
        {
            this.ObjectContext.IssueStatuses.AttachAsModified(currentIssueStatus, this.ChangeSet.GetOriginal(currentIssueStatus));
        }

        public void DeleteIssueStatus(IssueStatus issueStatus)
        {
            if ((issueStatus.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(issueStatus, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.IssueStatuses.Attach(issueStatus);
                this.ObjectContext.IssueStatuses.DeleteObject(issueStatus);
            }
        }

        public IQueryable<Project> GetProjects()
        {
            return ObjectContext.Projects.Include("Issues").Where(p => p.Status == 1).
                OrderBy(p => p.CreatedOn);
        }

        public void InsertProject(Project project)
        {
            project.CreatedOn = DateTime.Now;
            project.CreatedBy = Guid.Parse("449e73eb-2911-4302-abae-44c0b91d8d0c");
            project.Status = 1;
            if ((project.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(project, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Projects.AddObject(project);
            }
        }

        public void UpdateProject(Project currentProject)
        {
            this.ObjectContext.Projects.AttachAsModified(currentProject, this.ChangeSet.GetOriginal(currentProject));
        }

        public void DeleteProject(Project project)
        {
            if ((project.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(project, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.Projects.Attach(project);
                this.ObjectContext.Projects.DeleteObject(project);
            }
        }

        public IQueryable<TimeEntry> GetTimeEntries()
        {
            return this.ObjectContext.TimeEntries;
        }

        public void InsertTimeEntry(TimeEntry timeEntry)
        {
            timeEntry.CreatedOn = DateTime.Now;
            timeEntry.UserId = Guid.Parse("449e73eb-2911-4302-abae-44c0b91d8d0c");
            if ((timeEntry.EntityState != EntityState.Detached))
                ObjectContext.ObjectStateManager.ChangeObjectState(timeEntry, EntityState.Added);
            else
                ObjectContext.TimeEntries.AddObject(timeEntry);
        }

        public void UpdateTimeEntry(TimeEntry currentTimeEntry)
        {
            currentTimeEntry.UpdatedOn = DateTime.Now;
            ObjectContext.TimeEntries.AttachAsModified(currentTimeEntry, ChangeSet.GetOriginal(currentTimeEntry));
        }

        public void DeleteTimeEntry(TimeEntry timeEntry)
        {
            if ((timeEntry.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(timeEntry, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.TimeEntries.Attach(timeEntry);
                this.ObjectContext.TimeEntries.DeleteObject(timeEntry);
            }
        }

        public IQueryable<Tracker> GetTrackers()
        {
            return this.ObjectContext.Trackers;
        }

        public void InsertTracker(Tracker tracker)
        {
            if ((tracker.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(tracker, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Trackers.AddObject(tracker);
            }
        }

        public void UpdateTracker(Tracker currentTracker)
        {
            this.ObjectContext.Trackers.AttachAsModified(currentTracker, this.ChangeSet.GetOriginal(currentTracker));
        }

        public void DeleteTracker(Tracker tracker)
        {
            if ((tracker.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(tracker, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.Trackers.Attach(tracker);
                this.ObjectContext.Trackers.DeleteObject(tracker);
            }
        }


    }
}

