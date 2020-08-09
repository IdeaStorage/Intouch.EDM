using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Intouch.Edm.ViewModels
{
    public class HelperModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool isSelected = false;
        public string Name { get; set; }

        public int Id { get; set; }

        public string UserFullName { get; set; }

        public int UserId { get; set; }

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}