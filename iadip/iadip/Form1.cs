﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iadip {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
            TxtParser.StartReadFile += offClasterizationBtn;
            TxtParser.EndReadFile += onClasterizationBtn;
        }

        private void bLoadData_Click(object sender, EventArgs e) {
            dialogOpenDataFile.ShowDialog();
        }

        private void dialogOpenDataFile_FileOk(object sender, CancelEventArgs e)
        {
            IParser parser = new TxtParser();
            List<Apartament> apartments = parser.ReadFile(dialogOpenDataFile.FileName);

            // Прикрутить отрисовывалку результатов, потому что ничего не видно
            var clusters = new KMeans().Clasterize(apartments);

            foreach (var cluster in clusters)
            {
                Debug.WriteLine(cluster.Center);

                foreach (var r in cluster.Apartaments)
                    Debug.WriteLine(r);
            }
        }

        private void bTestEstimate_Click(object sender, EventArgs e) {
            double[] costs = new double [1000];
            for (int i = 0; i < costs.Length; i++) {
                costs[i] = i;
            }
            List<Cluster> clusters = new List<Cluster>();
            clusters.Add(new Cluster() {
                Center = new ClusterDataLerp() {
                    Cost = 500
                }
            });
            clusters.Add(new Cluster() {
                Center = new ClusterDataLerp() {
                    Cost = 700
                }
            });
            clusters.Add(new Cluster() {
                Center = new ClusterDataLerp() {
                    Cost = 100
                }
            });
            clusters.Add(new Cluster() {
                Center = new ClusterDataLerp() {
                    Cost = 300
                }
            });
            clusters.Add(new Cluster() {
                Center = new ClusterDataLerp() {
                    Cost = 900
                }
            });
            IEstimator estimator = new SimpleEstimator();
            Cluster result = new Cluster();
            Dictionary<double, double> dict = new Dictionary<double, double>();
            foreach (var i in costs) {
                result = estimator.Estimate(clusters, new SourceData() { Cost = i });
                dict.Add(i, result.Center.Cost);
            }
        }
        
        private void offClasterizationBtn() { bBeginClasterize.Visible = false; }
        private void onClasterizationBtn() { bBeginClasterize.Visible = true; }

        private void bBeginClasterize_Click(object sender, EventArgs e) {
            //Clasterize
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e) {
            TxtParser.StartReadFile -= offClasterizationBtn;
            TxtParser.EndReadFile -= onClasterizationBtn;
        }
    }
}
