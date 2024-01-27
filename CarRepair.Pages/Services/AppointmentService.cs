using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRepair.Pages.Models;
using Microsoft.VisualBasic;

namespace CarRepair.Pages.Services
{
    public class AppointmentService
    {
        // public List<Appointment>? Appointments
        // {
        //     get
        //     {
        //         return new List<Appointment>{
        //             new Appointment {
        //                 Id = 0,
        //                 VisitTime = DateTime.Now
        //             },
        //             new Appointment {
        //                 Id = 1,
        //                 VisitTime = DateTime.Now
        //             },
        //             new Appointment {
        //                 Id = 2,
        //                 VisitTime = DateTime.Now
        //             },
        //             new Appointment{
        //                 Id = 3,
        //                 VisitTime = DateTime.Now
        //             }
        //         };
        //     }
        //     set{}
        // }
        public List<Appointment> appointments = new List<Appointment>()
        {
            new Appointment {
                Id = 0,
                VisitTime = DateTime.Now
            },
            new Appointment {
                Id = 1,
                VisitTime = DateTime.Now
            },
            new Appointment {
                Id = 2,
                VisitTime = DateTime.Now
            },
            new Appointment{
                Id = 3,
                VisitTime = DateTime.Now
            }
        };
        // public List<Appointment>? GetAppointments()
        // {
        //     return Appointments;
        // }


        public List<Appointment> GetAppointment()
        {
            return appointments;
        }

        public void AddAppointment(Appointment appointment)
        {
            appointments.Add(appointment);
        }
    }

}