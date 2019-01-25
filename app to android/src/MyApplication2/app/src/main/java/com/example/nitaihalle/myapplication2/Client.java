package com.example.nitaihalle.myapplication2;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.Environment;

import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FilenameFilter;
import java.io.IOException;
import java.io.OutputStream;
import java.math.BigInteger;
import java.net.InetAddress;
import java.net.Socket;
import java.net.UnknownHostException;

public class Client {
    private Socket socket;
    private int port;
    private Pbar pb;
    private Context c;

    public Client(int p){
        socket = null;
        port = p;

    }
    public void connect(Pbar pb, Context con){
        try{
            //InetAddress serverAddr = InetAddress.getByName("10.0.0.2");
//            InetAddress serverAddr = InetAddress.getByName("192.168.86.72");
//            this.socket = new Socket(serverAddr,this.port);
//            OutputStream output = socket.getOutputStream();
            this.pb=pb;
            this.c=con;
            startTransfer();
        } catch (Exception e) {
            e.printStackTrace();
        }

    }
    public void startTransfer(){
        sendPhotos();
//        new Thread(){
//            @Override
//            public void run(){
//                sendPhotos();
//            }
//        }.start();
    }
    public byte[] getBytesFromBitmap(Bitmap bitmap) {
        ByteArrayOutputStream stream = new ByteArrayOutputStream();
        bitmap.compress(Bitmap.CompressFormat.PNG, 70, stream);
        return stream.toByteArray();
    }
    public void sendPic(File pic,int flag){
        try{

            FileInputStream fis = new FileInputStream(pic);
            Bitmap bitmap = BitmapFactory.decodeStream(fis);
            byte imgbyte [] = getBytesFromBitmap(bitmap);
            InetAddress serverAddr = InetAddress.getByName("10.0.0.2");
            //InetAddress serverAddr = InetAddress.getByName("192.168.86.72");//for my computer and my phone
            this.socket = new Socket(serverAddr,this.port);
            OutputStream output = socket.getOutputStream();
            byte[] name = pic.getName().getBytes();
            if (flag == -1){
                byte[] finish = "~finish~".getBytes();
                byte[] sizefinish = calcSize(BigInteger.valueOf(finish.length).toByteArray());
                output.write(sizefinish);
                output.flush();
                output.write(finish);
                output.flush();
                return;
            }
            byte[] sizeName = calcSize(BigInteger.valueOf(name.length).toByteArray());
            output.write(sizeName);
            output.flush();
            output.write(name);
            output.flush();

            byte [] picSize = calcSize(BigInteger.valueOf(imgbyte.length).toByteArray());
            output.write(picSize);
            output.flush();
            output.write(imgbyte);
            output.flush();


            //output.write(44);
            //output.write(imgbyte);
            //output.write(imgbyte);



        } catch (Exception e) {
            e.printStackTrace();
        }
    }
    private void sendPhotos(){
        File dcim = new File(Environment.getExternalStoragePublicDirectory(Environment.DIRECTORY_DCIM),"Camera");
        if (dcim == null) {
            return;
        }
        File[] pics = dcim.listFiles(new FilenameFilter() {
            @Override
            public boolean accept(File file, String s) {
                return (s.endsWith(".jpg")||s.endsWith(".png")||s.endsWith(".gif")||s.endsWith(".bmp"));
            }
        });
        pb.max=pics.length;
        new Thread(){
            @Override
            public void run(){
                pb.disp(c);
            }
        }.start();

        if(pics == null)
            return;
        int count =0;
        if (pics != null) {
            for (File pic : pics) {
                pb.up();

                sendPic(pic,1);
            }
            sendPic(pics[0],-1);
        }


    }
    private byte []calcSize( byte [] arr){
        if(4 == arr.length){
            return arr;
        }
        byte [] p = new byte[4];
        int j=arr.length-1;
        for(int i=3 ;i>=0 ;i--,j--){
            if(j>=0){
                p[i]=arr[j];
            }else{
                p[i]=0;
            }
        }
        return p;
    }

}
