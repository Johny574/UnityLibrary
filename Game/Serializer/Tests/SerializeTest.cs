
using System;
using NUnit.Framework;
using NUnit.Framework.Internal;

public class SerializeTests
{
    private SerializedMockTest _mock;
    private int testAmount = 5;

    [SetUp]
    public void SetUp(){
        _mock = new(testAmount);
    }

    [Test]
    public void MockSerialized(){ 
        Serializer.SaveFile(_mock, "Test", Serializer.SaveSlot.AutoSave);
        SerializedMockTest obj = Serializer.LoadFile<SerializedMockTest>("Test", Serializer.SaveSlot.AutoSave);
        Assert.That(obj.amount == testAmount);
    }
}

[Serializable]
public class SerializedMockTest
{
    public int amount;

    public SerializedMockTest(int amount){
        this.amount = amount;
    }
}