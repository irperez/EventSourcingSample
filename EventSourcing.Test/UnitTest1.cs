using EventSource.WebAPI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Runtime.CompilerServices;



namespace EventSourcing.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Create Event Store
            var memStore = new InMemoryEventStore();
            memStore.Init();
            memStore.AddSnapshot(new InMemorySnapshotStore<Regimen>());
            memStore.AddProjectionHandler(new InMemoryProjectionStore<RegimenIndex>());

            // Generate Entity ID Used to find in Entity Store
            var entityId = Guid.NewGuid();

            // Create an "AddedRecorded" event
            var newEvent = new AddedRecord() { Id = entityId, Name = "Ivan", Value = 42 };

            // Add event to Event Store.
            memStore.AppendEvent<Regimen>(entityId, newEvent);

            // Checking for behavior
            Assert.IsTrue(memStore.Events.Count > 0);
            Assert.AreSame(newEvent, memStore.Events[0].Data);
            Assert.AreNotEqual(Guid.Empty, memStore.Events[0].Id);
            Assert.AreEqual(memStore.Events[0].StreamType, memStore.Events[0].PartitionKey);
            Assert.AreEqual<ulong>(0, memStore.Events[0].Version);
            Assert.AreEqual("EventSourcing.AddedRecord", memStore.Events[0].Type);
            Assert.AreEqual(entityId, memStore.Events[0].EntityId);

            // Materialize Object from Event Store
            var actual = memStore.AggregateStream<Regimen>(entityId);

            // Validate
            Assert.AreEqual(newEvent.Id, actual.Id);
            Assert.AreEqual(newEvent.Name, actual.Name);
            Assert.AreEqual(newEvent.Value, actual.Value);

            // Add a child drug item to the root object via event
            var drugId = Guid.NewGuid();
            var addDrug = new AddedDrug() { Id = drugId, Dose = 500, GenericId = "Tylenol" };
            memStore.AppendEvent<Regimen>(entityId, addDrug);

            // Materialize object with drug from event store
            var actual2 = memStore.AggregateStream<Regimen>(entityId);
            Assert.AreEqual(addDrug.Dose, actual2.Drugs[0].Dose);
            Assert.AreEqual(addDrug.GenericId, actual2.Drugs[0].GenericId);

            // Add an update drug event
            var updateDrug = new UpdatedDrug() { Id = drugId, Dose = 1000, GenericId = "Tylenol" };
            memStore.AppendEvent<Regimen>(entityId, updateDrug);

            // Materialize object with updated drug
            var actual3 = memStore.AggregateStream<Regimen>(entityId); //This time the Apply method goes to the default switch option
            Assert.AreEqual(updateDrug.Dose, actual3.Drugs[0].Dose);
            Assert.AreEqual(updateDrug.GenericId, actual3.Drugs[0].GenericId);

            // Check Version
            Assert.AreEqual<ulong>(3, actual3.Version);


            for(int i = 0; i < 1000; i++)
            {
                var dId = Guid.NewGuid();
                var addDrug2 = new AddedDrug() { Id = dId, Dose = 500, GenericId = "Tylenol" };
                memStore.AppendEvent<Regimen>(entityId, addDrug2);

                var updateDrug2 = new UpdatedDrug() { Id = dId, Dose = 1000, GenericId = "Tylenol" };
                memStore.AppendEvent<Regimen>(entityId, updateDrug2);

                var rmDrg = new RemovedDrug() { Id = dId };
                memStore.AppendEvent<Regimen>(entityId, rmDrg);                
            }

            // Materialize Regimen object after all the looping.
            var finalRegimen = memStore.AggregateStream<Regimen>(entityId);

            // Materialize a Snapshot of the Regimen object
            var result = memStore.CreateSnapshot<Regimen>(entityId, 3);
            Assert.AreEqual(1, memStore.snapshots.Count);
            var snapshotStore = (InMemorySnapshotStore<Regimen>)memStore.snapshots[0];
            Assert.AreNotSame(actual, finalRegimen);
            Assert.AreEqual(1, snapshotStore.SnapshotData.Count);
            Assert.AreEqual(entityId, snapshotStore.SnapshotData[entityId][0].Id);

            // Materialize a Projection
            var resultProjection = memStore.CreateProjection<RegimenIndex>(entityId, 3);
            Assert.AreEqual(1, memStore.projections.Count);
            var projectionStore = (InMemoryProjectionStore<RegimenIndex>)memStore.projections[0];
            Assert.AreNotSame(actual, finalRegimen);
            Assert.AreEqual(1, projectionStore.ProjectionData.Count);
            Assert.AreEqual(entityId, projectionStore.ProjectionData[entityId][0].Id);
        }
    }

    

    public class UnitTestStore : InMemoryEventStore
    {

    }

    
}
