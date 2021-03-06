﻿namespace NHibernateDemo
{
    public class Location
    {
        public virtual string Street { get; set; }
        public virtual string City { get; set; }
        public virtual string Province { get; set; }
        public virtual string Country { get; set; }

        public override string ToString()
        {
            return $"{nameof(Street)}: {Street}, {nameof(City)}: {City}, {nameof(Province)}: {Province}, {nameof(Country)}: {Country}";
        }
    }
}