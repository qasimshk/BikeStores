using MassTransit;

namespace bs.component.sharedkernal.Extensions
{
    public static class CustomConfigurationExtensions
    {
        /// <summary>
        /// Should be used on every AddMassTransit configuration
        /// </summary>
        /// <param name="configurator"></param>
        /// 
        public static void ApplyCustomMassTransitConfiguration(this IBusRegistrationConfigurator configurator)
        {
            configurator.SetEndpointNameFormatter(new CustomEndpointNameFormatter());
        }
    }
}
