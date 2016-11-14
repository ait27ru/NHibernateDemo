using System;

namespace NHibernateDemo
{
    public class Customer
    {
        public virtual Guid Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual double AverageRating { get; set; }
        public virtual int? Points { get; set; }
        public virtual bool? HasGoldStatus { get; set; }
        public virtual DateTime? MemberSince { get; set; }
        public virtual CustomerCreditRating? CreditRating { get; set; }
        public virtual CustomerCreditRating? CreditRatingText { get; set; }
        public virtual Location Address { get; set; }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(FirstName)}: {FirstName}, {nameof(LastName)}: {LastName}, {nameof(AverageRating)}: {AverageRating}, {nameof(Points)}: {Points}, {nameof(HasGoldStatus)}: {HasGoldStatus}, {nameof(MemberSince)}: {MemberSince}, {nameof(CreditRating)}: {CreditRating}, {nameof(CreditRatingText)}: {CreditRatingText}, {nameof(Address)}: {Address}";
        }
    }

    public enum CustomerCreditRating
    {
        Excellent = 0,
        Good,
        Neutral,
        Poor,
        Terrible
    }
}