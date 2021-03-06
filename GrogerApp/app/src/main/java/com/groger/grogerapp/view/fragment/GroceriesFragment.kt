package com.groger.grogerapp.view.fragment

import android.app.FragmentTransaction
import android.arch.lifecycle.Observer
import android.arch.lifecycle.ViewModelProviders
import android.databinding.DataBindingUtil
import android.os.Bundle
import android.support.v4.app.Fragment
import android.support.v7.widget.LinearLayoutManager
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import com.groger.grogerapp.R
import com.groger.grogerapp.databinding.FragmentGroceriesBinding
import com.groger.grogerapp.service.model.Cluster
import com.groger.grogerapp.service.model.Grocery
import com.groger.grogerapp.view.adapter.GroceryAdapter
import com.groger.grogerapp.view.listener.groceries.GroceriesInteractionListener
import com.groger.grogerapp.viewmodel.GroceriesViewModel
import com.groger.grogerapp.viewmodel.factory.GroceriesViewModelFactory


// the fragment initialization parameters, e.g. ARG_ITEM_NUMBER
private const val ARG_TOKEN = "token"
private const val ARG_CLUSTER = "cluster"

/**
 * A simple [Fragment] subclass.
 * Activities that contain this fragment must implement the
 * [GroceriesFragment.OnFragmentInteractionListener] interface
 * to handle interaction events.
 * Use the [GroceriesFragment.newInstance] factory method to
 * create an instance of this fragment.
 *
 */


class GroceriesFragment : Fragment(), GroceriesInteractionListener {

    lateinit var binding : FragmentGroceriesBinding
    lateinit var viewModel : GroceriesViewModel
    private val groceryAdapter = GroceryAdapter(arrayListOf(), this)
    private var token: String? = null
    private var cluster: Cluster? = null

    override fun onRemoveGrocery(grocery: Grocery) {
        viewModel.removeGrocery(grocery)
        activity?.supportFragmentManager?.popBackStack()
    }

    override fun onGrocerySelected(grocery: Grocery) {
        activity?.supportFragmentManager!!.beginTransaction().apply {
            setTransition(FragmentTransaction.TRANSIT_FRAGMENT_FADE)
            replace(R.id.main_frame, GroceryFragment.newInstance(token!!, cluster!!, grocery))
            addToBackStack(null)
            commit()
        }
    }

    private fun onButtonAddClick(){
        activity?.supportFragmentManager!!.beginTransaction().apply {
            setTransition(FragmentTransaction.TRANSIT_FRAGMENT_FADE)
            replace(R.id.main_frame, AddGroceryFragment.newInstance(token!!, cluster!!))
            addToBackStack(null)
            commit()
        }
    }


    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        arguments?.let {
            token = it.getString(ARG_TOKEN)
            cluster = it.getSerializable(ARG_CLUSTER) as Cluster
        }
    }

    override fun onResume() {
        super.onResume()
        viewModel.loadClusterGroceries()
    }

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?,
                              savedInstanceState: Bundle?): View? {
        binding = DataBindingUtil.inflate(inflater,
                R.layout.fragment_groceries, container, false)

        val factory = GroceriesViewModelFactory(token!!, cluster!!)
        viewModel = ViewModelProviders.of(this, factory)
                .get(GroceriesViewModel::class.java)
        binding.viewModel = viewModel
        binding.executePendingBindings()

        binding.rvGroceries.layoutManager = LinearLayoutManager(this.context)

        binding.rvGroceries.adapter = groceryAdapter
        viewModel.groceries.observe(this, Observer { it?.let {
            groceryAdapter.replaceData(it)
        } })
        viewModel.loadClusterGroceries()

        binding.btnAdd.setOnClickListener {
            onButtonAddClick()
        }

        return binding.root
    }

    /*override fun onAttach(context: Context) {
        super.onAttach(context)
        if (context is OnFragmentInteractionListener) {
            listener = context
        } else {
            throw RuntimeException(context.toString() + " must implement OnFragmentInteractionListener")
        }
    }

    override fun onDetach() {
        super.onDetach()
        listener = null
    }*/

    companion object {
        /**
         * Use this factory method to create a new instance of
         * this fragment using the provided parameters.
         *
         * @param param1 Parameter 1.
         * @param param2 Parameter 2.
         * @return A new instance of fragment GroceriesFragment.
         */
        // TODO: Rename and change types and number of parameters
        @JvmStatic
        fun newInstance(token: String, custer: Cluster) =
                GroceriesFragment().apply {
                    arguments = Bundle().apply {
                        putString(ARG_TOKEN, token)
                        putSerializable(ARG_CLUSTER, custer)
                    }
                }
    }
}
