namespace ConnectionExample
{
    class Customer
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public override string ToString()
        {
            return $"[{Id}] {FirstName}";
        }
    }
}
