package com.example.nitaihalle.myapplication2;

import android.app.Notification;
import android.app.NotificationManager;
import android.content.Context;
import android.support.v4.app.NotificationCompat;

import static com.example.nitaihalle.myapplication2.R.*;

public class Pbar {
    public int max;
    public int curr;
    public Pbar(int m,int c){
        this.max=m;
        this.curr=c;
    }
    public void up(){
        curr++;
    }
    public boolean finish(){
        return (this.curr>=this.max);
    }
    public void disp(Context con){
        final NotificationCompat.Builder builder = new NotificationCompat.Builder(con,"transfer").setSmallIcon(drawable.ic_launcher_background).setContentTitle("sending images")
                .setPriority(NotificationCompat.PRIORITY_HIGH);
        final NotificationManager manager = (NotificationManager) con.getSystemService(con.NOTIFICATION_SERVICE);
        while(!finish()){
            builder.setProgress(this.max,this.curr,false);
            builder.setContentText(this.curr+" from "+this.max+"allready send");
            manager.notify(0,builder.build());
            try {
                Thread.sleep(750);
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
        }
        builder.setContentText("all images send");
        builder.setProgress(0,0,false);
        manager.notify(0,builder.build());
    }
}
