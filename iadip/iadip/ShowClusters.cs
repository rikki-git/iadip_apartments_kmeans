﻿using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace iadip
{
    public partial class ShowClusters : Form
    {
        public ShowClusters()
        {
            InitializeComponent();
        }

        public void Init(List<Cluster> clusters)
        {
            grid.DataSource = GetResultsTable(clusters);
        }

        DataTable GetResultsTable(List<Cluster> clusters)
        {
            DataTable table = new DataTable();

            string keyCluster = "Cluster";
            string keyId = "Id";
            string keyAreaSize = "Area size";
            string keyBathrooms = "Bathrooms";
            string keyCost = "Cost";
            string keyRooms = "Rooms";
            string keyCity = "City";
            string keyCompany = "Company";

            table.Columns.Add(keyCluster);
            table.Columns.Add(keyId);
            table.Columns.Add(keyCost);
            table.Columns.Add(keyAreaSize);
            table.Columns.Add(keyRooms);
            table.Columns.Add(keyBathrooms);
            table.Columns.Add(keyCity);
            table.Columns.Add(keyCompany);

            DataRow r = null;

            for (int i = 0; i < clusters.Count; i++)
            {
                Cluster c = clusters[i];

                r = table.NewRow();
                r[keyCluster] = "CENTER";
                r[keyId] = i;
                r[keyAreaSize] = string.Format("{0:0.00}", c.Center.AreaSize);
                r[keyBathrooms] = string.Format("{0:0.00}", c.Center.BathroomsCount);
                r[keyCost] = string.Format("{0:0.00}", c.Center.Cost);
                r[keyRooms] = string.Format("{0:0.00}", c.Center.RoomsCount);
                r[keyCity] = "-";
                r[keyCompany] = "-";
                table.Rows.Add(r);

                for (int j = 0; j < c.Apartaments.Count; j++)
                {
                    var a = c.Apartaments[j];
                    var data = a.Data;

                    r = table.NewRow();
                    r[keyId] = a.Id;
                    r[keyCluster] = i;
                    r[keyAreaSize] = data.AreaSize;
                    r[keyBathrooms] = data.BathroomsCount;
                    r[keyCost] = data.Cost;
                    r[keyRooms] = data.RoomsCount;
                    r[keyCity] = a.City;
                    r[keyCompany] = a.Company;
                    table.Rows.Add(r);
                }

                r = table.NewRow();
                table.Rows.Add(r);
            }
            
            return table;
        }
    }
}
