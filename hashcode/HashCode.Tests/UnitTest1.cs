using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HashCode.Tests
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			// ARRANGE
			var sut = CreateSUT();

			// ACT

			// ASSERT
			Assert.IsTrue(true);
		}

		private object CreateSUT()
		{
			return new object();
		}
	}
}
