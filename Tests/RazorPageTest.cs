using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class RazorPageTest : IClassFixture<RazorPageTestsFixture>
    {

        [Fact]
        public void Test()
        {
            using var ctx = new TestContext();
        }
    }
}
