﻿using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace BeehiveManagementSystem2._0
{
    class Queen:Bee
    {

        private Worker[] workers;
        private int shiftNumber = 0;

        public Queen(Worker[] workers,double weightMg)
            :base(weightMg)
        {
            this.workers = workers;
        }
        public bool AssignWork(string job, int shift)
        {
            for (int i = 0; i < workers.Length; i++)
            {
                if (workers[i].DoThisJob(job, shift))
                    return true;
            }
            return false;
        }

        public string WorkTheNextShift()
        {
            double totalConsumption = HoneyConsumptionRate();

            shiftNumber++;
            string report = $"Report for shift {shiftNumber}\r\n";
            for (int i = 0; i < workers.Length; i++)
            {
               totalConsumption += workers[i].HoneyConsumptionRate();
                if (workers[i].DidYouFinish())
                    report += $"Worker #{i + 1} finished the job\r\n";
                if (string.IsNullOrEmpty(workers[i].CurrentJob))
                    report += $"Worker #{i + 1} is not working\r\n";
                else
                {
                    if (workers[i].ShiftsLeft > 0)
                        report += $"Worker #{i + 1} is doing {workers[i].CurrentJob} for {workers[i].ShiftsLeft} more shifts\r\n";
                    else
                        report += $"Worker #{i + 1} will be done with {workers[i].CurrentJob} after this shift\r\n";
                }
            }
            
            report += $"Total honey xonsumed for the shift: {totalConsumption} units";

            return report;

        }
    }
}
