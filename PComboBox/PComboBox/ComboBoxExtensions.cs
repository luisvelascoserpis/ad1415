using Gtk;
using System;

namespace SerpisAd
{
	public static class ComboBoxExtensions {

		public static object GetId(this ComboBox comboBox) {
			TreeIter treeIter;
			bool activeIter = comboBox.GetActiveIter(out treeIter);
			return activeIter ? ((object[])comboBox.Model.GetValue(treeIter, 0))[0] : null;
		}

	}
}

