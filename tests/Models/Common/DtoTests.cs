namespace WS.Models
{
    using Contracts;
    using NUnit.Framework;

    public class DtoTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("", "")]
        [TestCase("_", "_")]
        [TestCase("fir", "fIr")]
        [TestCase("1", "1")]
        [TestCase("01", "01")]
        public void Entities_AreEqual(string id1, string id2)
        {
            var name = "Name";

            var pt = new PointDTO(0, 0, 0);

            var e1 = new EntityDTO(id1, name, pt);

            var e2 = new EntityDTO(id2, name, pt);

            Assert.That(e2, Is.EqualTo(e1));
        }
    }
}