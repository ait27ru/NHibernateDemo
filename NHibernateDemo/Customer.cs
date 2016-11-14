using System;

namespace NHibernateDemo
{
    public class Customer
    {
        public virtual int Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual double AverageRating { get; set; }
        public virtual int? Points { get; set; }
        public virtual bool? HasGoldStatus { get; set; }
        public virtual DateTime? MemberSince { get; set; }
        public virtual CustomerCreditRating? CreditRating { get; set; }
        public virtual CustomerCreditRating? CreditRatingText { get; set; }

        public override string ToString()
        {
            return
                $"{nameof(Id)}: {Id}, {nameof(FirstName)}: {FirstName}, {nameof(LastName)}: {LastName}, {nameof(Points)}: {Points}, {nameof(HasGoldStatus)}: {HasGoldStatus}, {nameof(MemberSince)}: {MemberSince}, {nameof(CreditRating)}: {CreditRating}, {nameof(CreditRatingText)}: {CreditRatingText}, {nameof(AverageRating)}: {AverageRating}";
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