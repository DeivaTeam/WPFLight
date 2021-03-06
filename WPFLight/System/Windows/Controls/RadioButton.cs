using System.Linq;
using System.Windows.Media;
using System.Windows.Controls.Primitives;

namespace System.Windows.Controls {
	public class RadioButton : ToggleButton {
        public RadioButton () { }

		#region Properties
        
        public static readonly DependencyProperty GroupNameProperty =
            DependencyProperty.Register(
                "GroupName", typeof(string), typeof(RadioButton));
        
        public string GroupName {
            get { return (string)GetValue(GroupNameProperty); }
            set { SetValue(GroupNameProperty, value); }
        }

		public static readonly DependencyProperty IsUncheckableProperty =
			DependencyProperty.Register(
				"IsUncheckable", typeof(bool), typeof(RadioButton));

		public bool IsUncheckable {
			get { return (bool)GetValue(IsUncheckableProperty); }
			set { SetValue(IsUncheckableProperty, value); }
		}

		#endregion

		protected override void OnCheckedChanged (bool chk) {
			if (chk) {
                // uncheck all RadioButtons with same group-name 
				var root = VisualTreeHelper.GetRoot (this);
				var controls = VisualTreeHelper.GetChildren<RadioButton> (root);
				foreach (var c in controls) {
                    if (c != this
                            && c.IsChecked
                            && c.GroupName != null
                            && c.GroupName == this.GroupName
                            && c.IsChecked ) {
                        c.IsChecked = false;
                    }
				}
			}
            base.OnCheckedChanged(chk);
		}

        protected override void OnClick () {
			RaiseClick ();
            if (this.Parent is Panel) {
                foreach (var c in ((Panel)this.Parent).Children.OfType<RadioButton>()) {
                    if (c != this && c.IsChecked && c.GroupName == this.GroupName) {
                        c.IsChecked = false;
                    }
                }
            }
			if (!this.IsChecked)
				this.IsChecked = true;
			else {
				if (this.IsUncheckable) {
					this.IsChecked = false;
				}
			}
        }
	}
}
