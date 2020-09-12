using Moq;
using ScavengerWorld.Units;
using ScavengerWorld.Units.Actions;
using ScavengerWorld.World;
using ScavengerWorld.World.Foods;
using System;
using Xunit;

namespace ScavengerWorldTest.Units
{
    /// <summary>
    /// A basic unit should be able to do the following:
    ///     - Gather Food
    ///         - If it is lower than limit and not already owned by this unit
    ///     - Drop Food
    ///         - If it is owned by this unit
    /// </summary>
    public class BasicUnitTests
    {
        [Fact]
        public void Take_CanAddFood_AddedToFoodSupply()
        {
            //Setup
            var subject = new BUnit(50);
            var quantity = new Random().Next(1, 50);
            var food = new Food(quantity, Food.FoodQuality.FAIR);

            //Act
            var canAdd = subject.Take(food);

            //Assert
            Assert.True(canAdd);
            Assert.Equal(quantity, subject.TotalFoodQuantity);
        }

        [Fact]
        public void Take_GatherLimitExceeded_CannotTake()
        {
            //Setup
            var subject = new BUnit(2);
            var quantity = new Random().Next(5, 50);
            var food = new Food(quantity, Food.FoodQuality.FAIR);

            //Act
            var canAdd = subject.Take(food);

            //Assert
            Assert.False(canAdd);
            Assert.Equal(0, subject.TotalFoodQuantity);
        }

        [Fact]
        public void Take_AlreadyOwnFood_CannotTakeAndNoChange()
        {
            //Setup
            var subject = new BUnit(500);
            var quantity = new Random().Next(1, 50);
            var food = new Food(quantity, Food.FoodQuality.FAIR);

            //Act
            subject.Take(food);
            var canAdd = subject.Take(food);

            //Assert
            Assert.False(canAdd);
            Assert.Equal(quantity, subject.TotalFoodQuantity);
        }

        [Fact]
        public void Take_NonFood_ReturnFalse()
        {
            //Setup
            var subject = new BUnit(0);
            var item = new Mock<ITransferable>();

            //Act
            var canTake = subject.Take(item.Object);

            //Assert
            Assert.False(canTake);
        }

        [Fact]
        public void Drop_DoNotOwnFood_CannotDrop()
        {
            //Setup
            var subject = new BUnit(50);
            var quantity = new Random().Next(1, 50);
            var food = new Food(quantity, Food.FoodQuality.FAIR);
            var altFood = new Food(quantity + 1, Food.FoodQuality.POOR);

            subject.Take(food);

            //Act
            var canDrop = subject.Drop(altFood);

            //Assert
            Assert.False(canDrop);
            Assert.Equal(quantity, subject.TotalFoodQuantity);
        }

        [Fact]
        public void Drop_FoodOwned_LessFood()
        {
            //Setup
            var subject = new BUnit(50);
            var quantity = new Random().Next(1, 50);
            var food = new Food(quantity, Food.FoodQuality.FAIR);

            subject.Take(food);

            //Act
            var canDrop = subject.Drop(food);

            //Assert
            Assert.True(canDrop);
            Assert.Equal(0, subject.TotalFoodQuantity);
        }

        [Fact]
        public void Drop_NonFood_ReturnFalse()
        {
            //Setup
            var subject = new BUnit(0);
            var item = new Mock<ITransferable>();

            //Act
            var canDrop = subject.Drop(item.Object);

            //Assert
            Assert.False(canDrop);
        }

        [Fact]
        public void CanGive_FoodNotOwned_ReturnFalse()
        {
            //Setup
            var subject = new BUnit();
            var action = new GiveAction(Guid.NewGuid(), Guid.NewGuid());

            //Act
            var canAct = subject.CanAttemptAction(action);

            //Assert
            Assert.False(canAct);
        }

        [Fact]
        public void CanGive_FoodOwned_ReturnTrue()
        {
            //Setup
            var subject = new BUnit(50);
            var quantity = new Random().Next(1, 50);
            var food = new Food(quantity, Food.FoodQuality.FAIR);
            var action = new GiveAction(food.Id, Guid.NewGuid());

            subject.Take(food);

            //Act
            var canAct = subject.CanAttemptAction(action);

            //Assert
            Assert.True(canAct);
        }

        [Fact]
        public void CanTake_NoSpaceForFood_ReturnFalse()
        {
            //Setup
            var subject = new BUnit(0);
            var action = new TakeAction(Guid.NewGuid());

            //Act
            var canAct = subject.CanAttemptAction(action);

            //Assert
            Assert.False(canAct);
        }

        [Fact]
        public void CanTake_SpaceForFood_ReturnTrue()
        {
            //Setup
            var subject = new BUnit(1);
            var action = new TakeAction(Guid.NewGuid());

            //Act
            var canAct = subject.CanAttemptAction(action);

            //Assert
            Assert.True(canAct);
        }

        [Fact]
        public void CanAct_DefaultActions_ReturnTrue()
        {
            var subject = new BUnit(1);

            var noop = new NoopAction();
            var attack = new AttackAction(Guid.NewGuid());
            var move = new MoveAction(MoveAction.Direction.EAST);
            Assert.True(subject.CanAttemptAction(noop));
            Assert.True(subject.CanAttemptAction(attack));
            Assert.True(subject.CanAttemptAction(move));
        }

    }

    class BUnit : BasicUnit
    {
        public BUnit()
        {

        }

        public BUnit(int gatherLimit)
        {
            GatherLimit = gatherLimit;
        }
    }
}
