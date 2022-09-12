namespace WebAPI.Entity
{
    public class TestEntity
    {
        public TestEntity()
        {
            Id = 1;
            Name = "testname";
            Age = 1;
            Test++;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public static int Test { get; set; }
    }
}
