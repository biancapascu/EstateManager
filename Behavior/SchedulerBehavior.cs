using EstateManager;
using Microsoft.Xaml.Behaviors;
using Syncfusion.UI.Xaml.Scheduler;
using System.Windows.Controls;

namespace WpfScheduler
{
    public class SchedulerBehavior : Behavior<Grid>
    {
        SfScheduler scheduler;

        protected override void OnAttached()
        {
            base.OnAttached();
            scheduler = this.AssociatedObject.FindName("Scheduler") as SfScheduler;
            this.OnWirEvents();
        }

        private void OnWirEvents()
        {
            this.scheduler.AppointmentEditorOpening += Scheduler_AppointmentEditorOpening;
        }

        private void Scheduler_AppointmentEditorOpening(object sender, AppointmentEditorOpeningEventArgs e)
        {
            e.Cancel = true;
            var editor = new AppointmentEditor(this.scheduler, e.Appointment, e.DateTime);
            editor.ShowDialog();
        }
        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.UnWireEvents();
            this.scheduler = null;
        }

        private void UnWireEvents()
        {
            this.scheduler.AppointmentEditorOpening -= Scheduler_AppointmentEditorOpening;
        }
    }
}
