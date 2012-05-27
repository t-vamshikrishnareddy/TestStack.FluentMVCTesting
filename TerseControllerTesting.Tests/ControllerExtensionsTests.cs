﻿using NUnit.Framework;
using TerseControllerTesting.Tests.TestControllers;

namespace TerseControllerTesting.Tests
{
    [TestFixture]
    class ControllerExtensionsShould
    {
        private TestController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new TestController();
        }

        [Test]
        public void Give_controller_modelstate_errors()
        {
            _controller.WithModelErrors();
            Assert.That(_controller.ModelState.IsValid, Is.False);
        }

        [Test]
        public void Call_action()
        {
            _controller.WithCallTo(c => c.SomeAction());
            Assert.That(_controller.SomeActionCalled);
        }

        [Test]
        public void Call_child_action()
        {
            _controller.WithCallToChild(c => c.SomeChildAction());
            Assert.That(_controller.SomeChildActionCalled);
        }

        [Test]
        public void Throw_exception_for_child_action_call_to_non_child_action()
        {
            var exception = Assert.Throws<InvalidControllerActionException>(() => _controller.WithCallToChild(c => c.SomeAction()));
            Assert.That(exception.Message, Is.EqualTo("Expected action SomeAction of controller TerseControllerTesting.Tests.TestControllers.TestController to be a child action, but it didn't have the ChildActionOnly attribute."));
        }
    }
}
