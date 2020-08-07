using Intouch.Edm.Models.Dtos.JobTiteDto;
using System;
using System.Collections.ObjectModel;
using EventType = Intouch.Edm.Models.Dtos.LookupDto.EventTypeLookup;
using ImpactArea = Intouch.Edm.Models.Dtos.LookupDto.ImpactAreaLookup;
using Location = Intouch.Edm.Models.Dtos.LookupDto.LocationLookup;

namespace Intouch.Edm.Services
{
    public class PickerService
    {
        public static ObservableCollection<ComboboxItem> GetSubject()
        {
            var subjects = new ObservableCollection<ComboboxItem>()
            {
                new ComboboxItem() { Id= 1 , Name="Acil Durum"},
                new ComboboxItem() { Id= 2 , Name="İş Sürekliliği"}
            };
            return subjects;
        }

        public static ObservableCollection<ComboboxItem> GetEvent(EventType.RootObject events)
        {
            var eventList = new ObservableCollection<ComboboxItem>();
            foreach (var source in events.result.items)
            {
                ComboboxItem sourceComboboxItem = new ComboboxItem();
                sourceComboboxItem.Name = source.eventType.name;
                sourceComboboxItem.Id = source.eventType.id;
                eventList.Add(sourceComboboxItem);
            }
            return eventList;
        }

        internal static ObservableCollection<ComboboxItem> GetSource(Models.Dtos.SourceLookupDto.RootObject sources)
        {
            var sourceList = new ObservableCollection<ComboboxItem>();
            foreach (var source in sources.result.items)
            {
                ComboboxItem sourceComboboxItem = new ComboboxItem();
                sourceComboboxItem.Name = source.source.name;
                sourceComboboxItem.Id = source.source.id;
                sourceList.Add(sourceComboboxItem);
            }

            return sourceList;
        }

        internal static ObservableCollection<ComboboxItem> GetImpactArea(ImpactArea.RootObject impactAreatList)
        {
            var impactAreaList = new ObservableCollection<ComboboxItem>();
            foreach (var impactArea in impactAreatList.result.items)
            {
                ComboboxItem impactAreaComboboxItem = new ComboboxItem();
                impactAreaComboboxItem.Name = impactArea.impactArea.name;
                impactAreaComboboxItem.Id = impactArea.impactArea.id;
                impactAreaList.Add(impactAreaComboboxItem);
            }

            return impactAreaList;
        }

        internal static ObservableCollection<ComboboxSelectedVersion> GetLocation(Location.RootObject locationList)
        {
            var locations = new ObservableCollection<ComboboxSelectedVersion>();
            foreach (var location in locationList.result.items)
            {
                ComboboxSelectedVersion locationComboboxItem = new ComboboxSelectedVersion();
                locationComboboxItem.Name = location.site.name;
                locationComboboxItem.Id = location.site.id;
                locationComboboxItem.IsSelected = location.nearest;
                locations.Add(locationComboboxItem);
            }

            return locations;
        }

        internal static ObservableCollection<ComboboxItem> GetTitles(Root titles)
        {
            var titleList = new ObservableCollection<ComboboxItem>();
            foreach (var title in titles.result.items)
            {
                ComboboxItem titleComboboxItem = new ComboboxItem();
                titleComboboxItem.Name = title.jobTitle.title;
                titleComboboxItem.Id = title.jobTitle.id;
                titleList.Add(titleComboboxItem);
            }

            return titleList;
        }

        internal static ObservableCollection<ComboboxItem> GetDepartments(Models.Dtos.UnitDto.Root departments)
        {
            var departmentList = new ObservableCollection<ComboboxItem>();
            foreach (var department in departments.result.items)
            {
                ComboboxItem departmentComboboxItem = new ComboboxItem();
                departmentComboboxItem.Name = department.unit.name;
                departmentComboboxItem.Id = department.unit.id;
                departmentList.Add(departmentComboboxItem);
            }

            return departmentList;
        }
    }

    public enum Subjects
    {
        Emergency = 1,
        BusinessContuniuty = 17
    }

    public enum Events
    {
        WaterFlood = 1,
        Fire = 2,
        Earthquake = 3,
        Pandemic = 5,
        Other = 6,
        BusinessContuniuty = 17
    }
}