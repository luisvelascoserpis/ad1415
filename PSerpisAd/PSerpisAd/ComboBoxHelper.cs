using Gtk;
using System;
using System.Data;

namespace SerpisAd
{
	public class ComboBoxHelper
	{
		public ComboBoxHelper (ComboBox comboBox, object id, string selectSql)	{
			CellRendererText cellRendererText = new CellRendererText ();
			comboBox.PackStart (cellRendererText, false);
			comboBox.SetCellDataFunc (cellRendererText, new CellLayoutDataFunc (delegate(CellLayout cell_layout, CellRenderer cell, TreeModel tree_model, TreeIter iter) {
				cellRendererText.Text = ((object[])tree_model.GetValue(iter, 0))[1].ToString();
			}));

			ListStore listStore = new ListStore (typeof(object));
			object[] initial = new object[] { null, "<sin asignar>" };
			TreeIter initialTreeIter = listStore.AppendValues ((object)initial);

			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = selectSql;
			IDataReader dataReader = dbCommand.ExecuteReader ();
			while (dataReader.Read()) {
				object currentId = dataReader [0];
				object currentName = dataReader [1];
				object[] values = new object[] { currentId, currentName };
				TreeIter treeIter = listStore.AppendValues ((object)values);
				if (currentId.Equals (id))
					initialTreeIter = treeIter;
			}
			dataReader.Close ();
			comboBox.Model = listStore;
			comboBox.SetActiveIter (initialTreeIter);
		}
	}
}

