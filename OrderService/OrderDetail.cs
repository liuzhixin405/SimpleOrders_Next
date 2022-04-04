namespace OrderService
{
    //public class OrderDetail
    //{      
    //    public string User { get; set; }
    //    public string Name { get; set; }
    //    public int Quantity { get; set; }
    //    public int ProductId { get; set; }
    //}

    public class OrderDetail
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string User { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string Name { get; set; }
     
        public int ProductId { get; set; }
    }
}
