namespace TingTingOS.Utils
{
    public class VMWareSVGADriver
    {
        public static void InitGUI()
        {
            Cosmos.HAL.Drivers.PCI.Video.VMWareSVGAII driver = new Cosmos.HAL.Drivers.PCI.Video.VMWareSVGAII();
            driver.SetMode(800, 600);
            driver.Clear(0x255);
        }
    }
}
