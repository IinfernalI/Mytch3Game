using System.Collections.Generic;
using Data;
using Enum;
using InGameStateMashine.InGameState;
using NUnit.Framework;

namespace Tests.EditMode
{
    public class TestCreateNewElements
    {
        [Test]
        public void TestCreateNewElementsSimplePasses()
        {
            // CreateNewElements createNewElements = new CreateNewElements(null, null);
            //
            // List<ElementData> elements = new()
            // {
            //     new ElementData(ElementType.Bear,1){xPos = 0, yPos = 0, State = ElementState.Idle},new ElementData(ElementType.Bear,2){xPos = 1, yPos = 0, State = ElementState.Idle},
            //     new ElementData(ElementType.Bear,3){xPos = 0, yPos = 1, State = ElementState.Idle},new ElementData(ElementType.Bear,4){xPos = 1, yPos = 1, State = ElementState.IsDead},
            //     new ElementData(ElementType.Bear,5){xPos = 0, yPos = 2, State = ElementState.Idle},new ElementData(ElementType.Bear,6){xPos = 1, yPos = 2, State = ElementState.IsDead},
            //     new ElementData(ElementType.Bear,7){xPos = 0, yPos = 3, State = ElementState.Idle},new ElementData(ElementType.Bear,8){xPos = 1, yPos = 3, State = ElementState.IsDead},
            //     new ElementData(ElementType.Bear,9){xPos = 0, yPos = 4, State = ElementState.Idle},new ElementData(ElementType.Bear,10){xPos = 1, yPos = 4, State = ElementState.Idle},
            //     
            // };
            // createNewElements.TestUpdateStateElements(elements);
            //
            // List<ElementData> assertElements = new()
            // {
            //    new ElementData(ElementType.Bear,1){xPos = 0, yPos = 0, State = ElementState.Idle},new ElementData(ElementType.Bear,8){xPos = 1, yPos = 0, State = ElementState.Idle},//0
            //                    new ElementData(ElementType.Bear,3){xPos = 0, yPos = 1, State = ElementState.Idle},new ElementData(ElementType.Bear,6){xPos = 1, yPos = 1, State = ElementState.IsDead},//1
            //                    new ElementData(ElementType.Bear,5){xPos = 0, yPos = 2, State = ElementState.Idle},new ElementData(ElementType.Bear,4){xPos = 1, yPos = 2, State = ElementState.IsDead},//2
            //                    new ElementData(ElementType.Bear,7){xPos = 0, yPos = 3, State = ElementState.Idle},new ElementData(ElementType.Bear,2){xPos = 1, yPos = 3, State = ElementState.IsDead},//3
            //                    new ElementData(ElementType.Bear,9){xPos = 0, yPos = 4, State = ElementState.Idle},new ElementData(ElementType.Bear,10){xPos = 1, yPos = 4, State = ElementState.Idle},
            //     
            // };
            //
            // Assert.That(elements,Is.EqualTo(assertElements));
            // // Use the Assert class to test conditions
        }
    
    }
}
