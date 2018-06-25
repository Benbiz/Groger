package com.groger.grogerapp.view.activity

import android.arch.lifecycle.ViewModelProviders
import android.content.Context
import android.content.Intent
import android.databinding.DataBindingUtil
import android.os.Bundle
import android.support.v7.app.AppCompatActivity
import android.util.Log
import com.groger.grogerapp.R
import com.groger.grogerapp.databinding.ActivityLoginBinding
import com.groger.grogerapp.service.model.UserToken
import com.groger.grogerapp.view.listener.SigninResultCallback
import com.groger.grogerapp.viewmodel.LoginViewModel
import com.groger.grogerapp.viewmodel.factory.LoginViewModelFactory
import es.dmoral.toasty.Toasty
import java.util.*

class LoginActivity : AppCompatActivity(), SigninResultCallback {

    override fun onSuccess(token: UserToken?) {
        val date = Date()

        Log.i("User token", "Expires in: " + Date(date.time + token?.expires_in!! * 1000).toString())
        getPreferences(Context.MODE_PRIVATE).edit().apply({
            putString("TOKEN.ACCESS_TOKEN", token?.access_token)
            putInt("TOKEN.EXPIRES_IN", token?.expires_in!!)
            putLong("TOKEN.EXPIRES_AT", (date.time + token?.expires_in!! * 1000))
            putString("TOKEN.TOKEN_TYPE", token.token_type)
            apply()
        })
        loggedIn(token!!)
    }

    private fun loggedIn(token : UserToken)
    {
        Toasty.success(this, "Logged in successfully").show()
        val intent = Intent(this, ClusterActivity::class.java).apply {
            putExtra("token", "${token?.token_type} ${token?.access_token}")
        }
        startActivity(intent)
    }

    override fun onError(reason: String) {
        Toasty.error(this, reason).show()
    }

    lateinit var binding: ActivityLoginBinding

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        val accessToken = getPreferences(Context.MODE_PRIVATE).getString("TOKEN.ACCESS_TOKEN", "none")
        if (accessToken != "none")
        {
            val expiresIn = getPreferences(Context.MODE_PRIVATE).getInt("TOKEN.EXPIRES_IN", 0)
            val expiresAt = getPreferences(Context.MODE_PRIVATE).getLong("TOKEN.EXPIRES_AT", 0)
            val date = Date(expiresAt)
            if (date > Date()) {
                val tokenType = getPreferences(Context.MODE_PRIVATE).getString("TOKEN.TOKEN_TYPE", "")
                loggedIn(UserToken(accessToken, tokenType, expiresIn))
                return
            }
            getPreferences(Context.MODE_PRIVATE).edit().apply({
                remove("TOKEN.ACCESS_TOKEN")
                remove("TOKEN.EXPIRES_IN")
                remove("TOKEN.EXPIRES_AT")
                remove("TOKEN.TOKEN_TYPE")
                apply()
            })
        }


        val factory = LoginViewModelFactory(this)
        val vm = ViewModelProviders.of(this, factory)
                .get(LoginViewModel::class.java)
        binding = DataBindingUtil.setContentView(this, R.layout.activity_login)
        binding.apply {
            viewModel = vm
        }
    }
}
