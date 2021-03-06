﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace CBMicro.Behaviours
{
    public class SampleBehaviour : Behavior<UIElement>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            (AssociatedObject as Button).PreviewMouseLeftButtonDown += SampleBehaviour_PreviewMouseLeftButtonDown;
        }

        private void SampleBehaviour_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            (AssociatedObject as Button).PreviewMouseLeftButtonDown -= SampleBehaviour_PreviewMouseLeftButtonDown;
        }

    }

    public class Behaviors : List<Behavior>
    {                              
    }

    public class Triggers : List<System.Windows.Interactivity.TriggerBase>
    {
    }

    public static class SupplementaryInteraction
    {
        public static Behaviors GetBehaviors(DependencyObject obj)
        {
            return (Behaviors)obj.GetValue(BehaviorsProperty);
        }

        public static void SetBehaviors(DependencyObject obj, Behaviors value)
        {
            obj.SetValue(BehaviorsProperty, value);
        }

        public static readonly DependencyProperty BehaviorsProperty =
            DependencyProperty.RegisterAttached("Behaviors", typeof(Behaviors), typeof(SupplementaryInteraction), new UIPropertyMetadata(null, OnPropertyBehaviorsChanged));

        private static void OnPropertyBehaviorsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var behaviors = Interaction.GetBehaviors(d);
            foreach (var behavior in e.NewValue as Behaviors) behaviors.Add(behavior);
        }

        public static Triggers GetTriggers(DependencyObject obj)
        {
            return (Triggers)obj.GetValue(TriggersProperty);
        }

        public static void SetTriggers(DependencyObject obj, Triggers value)
        {
            obj.SetValue(TriggersProperty, value);
        }

        public static readonly DependencyProperty TriggersProperty =
            DependencyProperty.RegisterAttached("Triggers", typeof(Triggers), typeof(SupplementaryInteraction), new UIPropertyMetadata(null, OnPropertyTriggersChanged));

        private static void OnPropertyTriggersChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var triggers = Interaction.GetTriggers(d);
            foreach (var trigger in e.NewValue as Triggers) triggers.Add(trigger);
        }
    }
}
