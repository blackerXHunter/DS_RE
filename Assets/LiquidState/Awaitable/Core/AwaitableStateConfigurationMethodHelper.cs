﻿// Author: Prasanna V. Loganathar
// Created: 12:20 16-07-2015
// Project: LiquidState
// License: http://www.apache.org/licenses/LICENSE-2.0

using System;
using LiquidState.Core;
using LiquidState.Common;

namespace LiquidState.Awaitable.Core
{
    internal static class AwaitableStateConfigurationMethodHelper
    {
        internal static AwaitableStateConfiguration<TState, TTrigger> OnEntry<TState, TTrigger>(
            AwaitableStateConfiguration<TState, TTrigger> config, object action, AwaitableTransitionFlag flags)
        {
            config.CurrentStateRepresentation.OnEntryAction = action;
            config.CurrentStateRepresentation.AwaitableTransitionFlags |= flags;

            return config;
        }

        internal static AwaitableStateConfiguration<TState, TTrigger> OnExit<TState, TTrigger>(
            AwaitableStateConfiguration<TState, TTrigger> config, object action, AwaitableTransitionFlag flags)
        {
            config.CurrentStateRepresentation.OnExitAction = action;
            config.CurrentStateRepresentation.AwaitableTransitionFlags |= flags;

            return config;
        }

        internal static AwaitableStateConfiguration<TState, TTrigger> Permit<TState, TTrigger>(
            AwaitableStateConfiguration<TState, TTrigger> config, object predicate, TTrigger trigger,
            TState resultingState, object onTriggerAction, AwaitableTransitionFlag flags)
        {
            Contract.NotNull(trigger != null, nameof(trigger));
            Contract.NotNull(resultingState != null, nameof(resultingState));

            if (
                AwaitableStateConfigurationHelper.FindTriggerRepresentation(trigger,
                    config.CurrentStateRepresentation) != null) ExceptionHelper.ThrowExclusiveOperation();

            var rep = AwaitableStateConfigurationHelper.CreateTriggerRepresentation(trigger,
                config.CurrentStateRepresentation);

            rep.NextStateRepresentationWrapper =
                AwaitableStateConfigurationHelper.FindOrCreateStateRepresentation(resultingState,
                    config.Representations);
            rep.OnTriggerAction = onTriggerAction;
            rep.ConditionalTriggerPredicate = predicate;
            rep.AwaitableTransitionFlags |= flags;

            return config;
        }

        internal static AwaitableStateConfiguration<TState, TTrigger> Ignore<TState, TTrigger>(
            AwaitableStateConfiguration<TState, TTrigger> config, object predicate, TTrigger trigger,
            AwaitableTransitionFlag flags)
        {
            Contract.NotNull(trigger != null, nameof(trigger));

            if (
                AwaitableStateConfigurationHelper.FindTriggerRepresentation(trigger, config.CurrentStateRepresentation) !=
                null) ExceptionHelper.ThrowExclusiveOperation();

            var rep = AwaitableStateConfigurationHelper.CreateTriggerRepresentation(trigger,
                config.CurrentStateRepresentation);
            rep.NextStateRepresentationWrapper = null;
            rep.ConditionalTriggerPredicate = predicate;
            rep.AwaitableTransitionFlags |= flags;

            return config;
        }

        internal static AwaitableStateConfiguration<TState, TTrigger> PermitDynamic<TState, TTrigger>(
            AwaitableStateConfiguration<TState, TTrigger> config, TTrigger trigger,
            object targetStateFunc,
            object onTriggerAction, AwaitableTransitionFlag flags)
        {
            Contract.NotNull(trigger != null, nameof(trigger));
            Contract.NotNull(targetStateFunc != null, nameof(targetStateFunc));

            if (
                AwaitableStateConfigurationHelper.FindTriggerRepresentation(trigger, config.CurrentStateRepresentation) !=
                null) ExceptionHelper.ThrowExclusiveOperation();
            var rep = AwaitableStateConfigurationHelper.CreateTriggerRepresentation(trigger,
                config.CurrentStateRepresentation);
            rep.NextStateRepresentationWrapper = targetStateFunc;
            rep.OnTriggerAction = onTriggerAction;
            rep.ConditionalTriggerPredicate = null;
            rep.AwaitableTransitionFlags |= flags;

            return config;
        }
    }
}