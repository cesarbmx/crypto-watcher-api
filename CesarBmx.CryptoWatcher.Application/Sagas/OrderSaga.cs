using System;
using MassTransit;
using CesarBmx.Shared.Messaging.Ordering.Events;
using CesarBmx.Shared.Messaging.Notification.Commands;

namespace CesarBmx.Ordering.Application.Sagas
{
    public class OrderSagaState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public int CurrentState { get; set; }

        public Guid OrderId { get; set; }
        public DateTime? SubmittedAt { get; set; }
        public DateTime? PlacedAt { get; set; }
        public DateTime? FilledAt { get; set; }
        public DateTime? CancelledAt { get; set; }
        public DateTime? ExpiredAt { get; set; }
        public DateTime? NotifiedAt { get; set; }
    }

    public class OrderSaga : MassTransitStateMachine<OrderSagaState>
    {
        public OrderSaga()
        {
            InstanceState(x => x.CurrentState, Pending, Placed, Filled, Cancelled, Expired);

            Event(() => OrderSubmitted, x => x.CorrelateById(m => m.Message.OrderId));
            Event(() => OrderPlaced, x => x.CorrelateById(m => m.Message.OrderId));
            Event(() => OrderFilled, x => x.CorrelateById(m => m.Message.OrderId));
            Event(() => OrderCancelled, x => x.CorrelateById(m => m.Message.OrderId));
            Event(() => OrderExpired, x => x.CorrelateById(m => m.Message.OrderId));

            Initially(
                 When(OrderSubmitted)
                    .SetSubmissionDetails()
                    .SendNotification()
                    .TransitionTo(Pending));

            During(Pending,
                When(OrderPlaced)
                    .SetPlacingDetails()
                    .SendNotification()
                    .TransitionTo(Placed));

            During(Placed,
               When(OrderFilled)
                   .SetFillingDetails()
                   .SendNotification()
                   .TransitionTo(Filled)
                   .Finalize());         

            During(Placed,
               When(OrderCancelled)
                   .SetCancelationDetails()
                   .SendNotification()
                   .TransitionTo(Cancelled)
                   .Finalize());

            During(Placed,
              When(OrderExpired)
                  .SetExpirationDetails()
                  .SendNotification()
                  .TransitionTo(Expired)
                  .Finalize());            
        }

        public Event<OrderSubmitted> OrderSubmitted { get; private set; }
        public Event<OrderPlaced> OrderPlaced { get; private set; }
        public Event<OrderFilled> OrderFilled { get; private set; }
        public Event<OrderCancelled> OrderCancelled { get; private set; }
        public Event<OrderExpired> OrderExpired { get; private set; }

        public State Pending { get; private set; }
        public State Placed { get; private set; }
        public State Filled { get; private set; }
        public State Cancelled { get; private set; }
        public State Expired { get; private set; }
    }

    public static class OrderSagaExtensions
    {
        public static EventActivityBinder<OrderSagaState, OrderSubmitted> SetSubmissionDetails(
            this EventActivityBinder<OrderSagaState, OrderSubmitted> binder)
        {
            return binder.Then(x =>
            {
                x.Saga.OrderId = x.Message.OrderId;
                x.Saga.SubmittedAt = x.Message.SubmittedAt;
            });
        }
        public static EventActivityBinder<OrderSagaState, OrderPlaced> SetPlacingDetails(
           this EventActivityBinder<OrderSagaState, OrderPlaced> binder)
        {
            return binder.Then(x =>
            {
                x.Saga.PlacedAt = x.Message.PlacedAt;
            });
        }
        public static EventActivityBinder<OrderSagaState, OrderFilled> SetFillingDetails(
          this EventActivityBinder<OrderSagaState, OrderFilled> binder)
        {
            return binder.Then(x =>
            {
                x.Saga.FilledAt = x.Message.FilledAt;
            });
        }
        public static EventActivityBinder<OrderSagaState, OrderCancelled> SetCancelationDetails(
          this EventActivityBinder<OrderSagaState, OrderCancelled> binder)
        {
            return binder.Then(x =>
            {
                x.Saga.CancelledAt = x.Message.CancelledAt;
            });
        }
        public static EventActivityBinder<OrderSagaState, OrderExpired> SetExpirationDetails(
          this EventActivityBinder<OrderSagaState, OrderExpired> binder)
        {
            return binder.Then(x =>
            {
                x.Saga.CancelledAt = x.Message.ExpiredAt;
            });
        }

        public static EventActivityBinder<OrderSagaState, OrderSubmitted> SendNotification(
         this EventActivityBinder<OrderSagaState, OrderSubmitted> binder)
        {
            var response = binder.RespondAsync(context => context.Init<SendMessage>(new SendMessage
            {
                MessageId = Guid.NewGuid(),
                UserId = context.Message.UserId,
                Text = "Order submitted"
            }));

            return response;
        }
        public static EventActivityBinder<OrderSagaState, OrderPlaced> SendNotification(
          this EventActivityBinder<OrderSagaState, OrderPlaced> binder)
        {
            var response = binder.RespondAsync(context => context.Init<SendMessage>(new SendMessage
            {
                MessageId = Guid.NewGuid(),
                UserId = context.Message.UserId,
                Text = "Order placed"
            }));

            return response;
        }
        public static EventActivityBinder<OrderSagaState, OrderFilled> SendNotification(
         this EventActivityBinder<OrderSagaState, OrderFilled> binder)
        {
            return binder.RespondAsync(context => context.Init<SendMessage>(new SendMessage
            {
                MessageId = Guid.NewGuid(),
                UserId = context.Message.UserId,
                Text = "Order filled"
            }));
        }
        public static EventActivityBinder<OrderSagaState, OrderCancelled> SendNotification(
        this EventActivityBinder<OrderSagaState, OrderCancelled> binder)
        {
            return binder.RespondAsync(context => context.Init<SendMessage>(new SendMessage
            {
                MessageId = Guid.NewGuid(),
                UserId = context.Message.UserId,
                Text = "Order cancelled"
            }));
        }
        public static EventActivityBinder<OrderSagaState, OrderExpired> SendNotification(
        this EventActivityBinder<OrderSagaState, OrderExpired> binder)
        {
            return binder.RespondAsync(context => context.Init<SendMessage>(new SendMessage
            {
                MessageId = Guid.NewGuid(),
                UserId = context.Message.UserId,
                Text = "Order expired"
            }));
        }

    }
}
