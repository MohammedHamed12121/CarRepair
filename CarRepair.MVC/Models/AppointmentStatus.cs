namespace CarRepair.MVC.Models
{
    public enum AppointmentStatus
    {
        NotSeen,       // when sendin the appointment
        Seen,       // when the receptionist sent reply with the price
        Accept,      // when the user accept the reply
        Rejected     // if the problem can't be handuled by the company
    }
}