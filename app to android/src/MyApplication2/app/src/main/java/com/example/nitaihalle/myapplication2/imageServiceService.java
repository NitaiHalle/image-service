package com.example.nitaihalle.myapplication2;

import android.app.Service;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.net.wifi.WifiManager;
import android.os.IBinder;
import android.support.annotation.Nullable;
import android.util.Log;
import android.widget.ProgressBar;
import android.widget.Toast;

public class imageServiceService extends Service {
    private BroadcastReceiver Receiver;
    @Nullable
    @Override
    public IBinder onBind(Intent intent) {
        Log.d("STATE","in create");
        return null;
    }
    @Override
    public void onCreate() {
        super.onCreate();
        Log.d("STATE","in create");
        final IntentFilter theFilter = new IntentFilter();
        theFilter.addAction("android.net.wifi.supplicant.CONNECTION_CHANGE");
        theFilter.addAction("android.net.wifi.STATE_CHANGE");
        this.Receiver = new BroadcastReceiver() {
            @Override
            public void onReceive(Context context, Intent intent) {
                WifiManager wifiManager = (WifiManager) context.getSystemService(Context.WIFI_SERVICE);
                NetworkInfo networkInfo = intent.getParcelableExtra(WifiManager.EXTRA_NETWORK_INFO);
                if (networkInfo != null) {
                    if (networkInfo.getType() == ConnectivityManager.TYPE_WIFI) {//get the different network states
                        if (networkInfo.getState() == NetworkInfo.State.CONNECTED) {
                            startTransfer();            // Starting the Transfer
                        }
                     }
                }
             }
        };// Registers the receiver so that your service will listen for broadcasts
        this.registerReceiver(this.Receiver, theFilter);
    }
    @Override
    public int onStartCommand(Intent intent, int flag, int startId){
        Log.d("STATE","in create");
        Toast.makeText(this,"Service starting...", Toast.LENGTH_SHORT).show();
        return START_STICKY;
    }
    public void startTransfer(){
        final Context con =this;
        new Thread(){
            @Override
            public void run(){
                Client client = new Client(8888);
                Pbar pb = new Pbar(0,0);

                try{
                    client.connect(pb,con);
                }catch (Exception e){
                    return;
                }
             //   pb.disp(con);
               // client.startTransfer();
            }
        }.start();
    }
    public void onDestroy() {
        Log.d("STATE","in create");
        Toast.makeText(this,"Service ending...", Toast.LENGTH_SHORT).show();
    }



}
