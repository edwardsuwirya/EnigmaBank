namespace Domain.Extensions;

public static class AccountHolderExt
{
    public static AccountHolder Update(this AccountHolder accountHolder, string firstName, String lastName,
        string contactNumber, string email)
    {
        if (firstName is not null &&
            accountHolder.FirstName.Equals(firstName, StringComparison.CurrentCultureIgnoreCase) is not true)
            accountHolder.FirstName = firstName;

        if (lastName is not null &&
            accountHolder.LastName.Equals(lastName, StringComparison.CurrentCultureIgnoreCase) is not true)
            accountHolder.LastName = lastName;

        if (contactNumber is not null &&
            accountHolder.ContactNumber.Equals(contactNumber, StringComparison.CurrentCultureIgnoreCase) is not true)
            accountHolder.ContactNumber = contactNumber;

        if (email is not null &&
            accountHolder.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase) is not true)
            accountHolder.Email = email;
        return accountHolder;
    }
}