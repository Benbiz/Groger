package com.groger.grogerapp.view.activity

import android.app.FragmentTransaction.TRANSIT_FRAGMENT_FADE
import android.net.Uri
import android.os.Bundle
import android.support.v7.app.AppCompatActivity
import com.groger.grogerapp.view.fragment.HomeFragment
import com.groger.grogerapp.R
import com.groger.grogerapp.R.id.*
import com.groger.grogerapp.service.model.Cluster
import com.groger.grogerapp.view.fragment.GroceriesFragment
import com.groger.grogerapp.view.fragment.NotificationFragment
import com.groger.grogerapp.view.fragment.ShoppinglistFragment
import com.groger.grogerapp.view.ui.setForceShiftingMode
import kotlinx.android.synthetic.main.activity_main.*

class MainActivity : AppCompatActivity(), HomeFragment.OnFragmentInteractionListener, GroceriesFragment.OnFragmentInteractionListener {
    override fun onFragmentInteraction(uri: Uri) {
        TODO("not implemented") //To change body of created functions use File | Settings | File Templates.
    }

    lateinit var token: String
    lateinit var cluster : Cluster

    private val shoppinglistFragment: ShoppinglistFragment = ShoppinglistFragment()
    private val notificationFragment : NotificationFragment = NotificationFragment()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        token = intent.getStringExtra("token")
        cluster = intent.getSerializableExtra("cluster") as Cluster

        nav_bottom.setForceShiftingMode(true)
        setFragment(HomeFragment.newInstance(token, cluster))
        nav_bottom.setOnNavigationItemSelectedListener {
            when (it.itemId) {
                nav_home ->
                {
                    setFragment(HomeFragment.newInstance(token, cluster))
                    true
                }
                nav_groceries ->
                {
                    setFragment(GroceriesFragment.newInstance(token, cluster))
                    true
                }
                nav_shoppinglists ->{
                    setFragment(shoppinglistFragment)
                    true
                }
                nav_notification ->
                {
                    setFragment(notificationFragment)
                    true
                }
                else -> {
                    false
                }
            }
        }
    }

    private fun setFragment(fragment : android.support.v4.app.Fragment)
    {
        val fragmentTransaction = supportFragmentManager.beginTransaction().apply {
            setTransition(TRANSIT_FRAGMENT_FADE)
            replace(R.id.main_frame, fragment)
            commit()
        }


    }
}
