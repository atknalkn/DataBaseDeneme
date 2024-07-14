namespace HelloWorld.Models{
    public class Computer{
        public string Motherboard{get;set;}
        public int CPUCores {get;set;}
        public bool HasWifi {get;set;}
        public bool HasLTE {get;set;}
        public DateTime RelaseDate{ get;set;}
        public float Price{get;set;}
        public string VideoCard{get;set;}

        public Computer(){
        if (VideoCard==null){
            VideoCard="";
            
        }
        if(Motherboard==null){
            Motherboard="";
        }
    }
    }
    
    
}