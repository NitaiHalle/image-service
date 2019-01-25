package com.example.nitaihalle.myapplication2;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;


public class MainActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
    }
    public void onStart(View view){
        Button b1 = (Button)findViewById(R.id.button);
        Button b2 = (Button)findViewById(R.id.button2);
        Intent intent = new Intent(this,imageServiceService.class);
        b1.setVisibility(View.GONE);
        startService(intent);
        Log.d("STATE","chcck");
    }
    public void onStop(View view){
        Button b1 = (Button)findViewById(R.id.button);
        Button b2 = (Button)findViewById(R.id.button2);
        Intent intent = new Intent(this,imageServiceService.class);
        b1.setVisibility(View.VISIBLE);
        stopService(intent);
    }
}
