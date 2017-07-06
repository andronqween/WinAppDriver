﻿//******************************************************************************
//
// Copyright (c) 2016 Microsoft Corporation. All rights reserved.
//
// This code is licensed under the MIT License (MIT).
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
//******************************************************************************

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace W3CWebDriver
{
    [TestClass]
    public class TouchClick : EdgeBase
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            Setup(context);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            TearDown();
        }

        [TestMethod]
        public void SingleTap()
        {
            session.FindElementByAccessibilityId("addressEditBox").SendKeys(CommonTestSettings.EdgeAboutFlagsURL + OpenQA.Selenium.Keys.Enter);
            System.Threading.Thread.Sleep(1000); // Sleep for 1 second
            var originalTitle = session.Title;
            Assert.AreNotEqual(string.Empty, originalTitle);

            // Navigate to Edge blank page to create navigation history
            session.FindElementByAccessibilityId("addressEditBox").SendKeys(CommonTestSettings.EdgeAboutBlankURL + OpenQA.Selenium.Keys.Enter);
            Assert.AreNotEqual(originalTitle, session.Title);

            // Perform single tap touch on the back button
            touchScreen.SingleTap(session.FindElementByName("Back").Coordinates);
            System.Threading.Thread.Sleep(1000); // Sleep for 1 second

            // Make sure the page you went to is the page we started on
            Assert.AreEqual(originalTitle, session.Title);
        }

        [TestMethod]
        public void ErrorTouchStaleElement()
        {
            try
            {
                // Perform single tap touch on stale element
                touchScreen.SingleTap(GetStaleElement().Coordinates);
                Assert.Fail("Exception should have been thrown");
            }
            catch (System.InvalidOperationException exception)
            {
                Assert.AreEqual(ErrorStrings.StaleElementReference, exception.Message);
            }
        }
    }
}
