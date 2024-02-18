namespace CarRepair.Pages.Models
{
    public enum Status
    {
        AppointmentAccepted,  // the default when the user accept the confirmation form the company
        Recieved,   // when the user send the car to the company
        UnderRepair,  // when the employee begin to work on the fix
        Finished,     // When the employee finish, and the car is ready for recieve
        Paid
    }
}