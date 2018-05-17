package com.groger.grogerapp

import android.arch.lifecycle.ViewModelProviders
import android.content.Intent
import android.databinding.DataBindingUtil
import android.support.v7.app.AppCompatActivity
import android.os.Bundle
import com.groger.grogerapp.databinding.ActivityLoginBinding
import com.groger.grogerapp.service.model.UserToken
import com.groger.grogerapp.view.callback.SigninResultCallback
import com.groger.grogerapp.viewmodel.LoginViewModel
import com.groger.grogerapp.viewmodel.LoginViewModelFactory
import es.dmoral.toasty.Toasty
import io.reactivex.disposables.CompositeDisposable

class LoginActivity : AppCompatActivity(), SigninResultCallback {

    override fun onSuccess(token: UserToken?) {
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

        val factory =  LoginViewModelFactory(this)
        val vm = ViewModelProviders.of(this, factory)
                .get(LoginViewModel::class.java)
        binding = DataBindingUtil.setContentView(this, R.layout.activity_login)
        binding.apply {
            viewModel = vm
        }
    }
}
