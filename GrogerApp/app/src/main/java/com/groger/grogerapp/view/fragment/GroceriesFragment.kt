package com.groger.grogerapp.view.fragment

import android.arch.lifecycle.Observer
import android.arch.lifecycle.ViewModelProviders
import android.content.Context
import android.net.Uri
import android.os.Bundle
import android.support.v4.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import com.groger.grogerapp.R
import com.groger.grogerapp.databinding.FragmentGroceriesBinding
import com.groger.grogerapp.service.model.Cluster
import com.groger.grogerapp.service.model.Grocery
import com.groger.grogerapp.view.adapter.GroceryAdapter
import android.databinding.DataBindingUtil
import android.support.v7.widget.LinearLayoutManager
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


class GroceriesFragment : Fragment(), GroceryAdapter.OnItemClickListener {

    lateinit var binding : FragmentGroceriesBinding
    private val groceryAdapter = GroceryAdapter(arrayListOf(), this)
    private var token: String? = null
    private var cluster: Cluster? = null
    private var listener: OnFragmentInteractionListener? = null

    override fun onItemClick(grocery: Grocery) {
        TODO("not implemented") //To change body of created functions use File | Settings | File Templates.
    }

    override fun onItemLongClick(grocery: Grocery) {
        TODO("not implemented") //To change body of created functions use File | Settings | File Templates.
    }


    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        arguments?.let {
            token = it.getString(ARG_TOKEN)
            cluster = it.getSerializable(ARG_CLUSTER) as Cluster
        }
    }

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?,
                              savedInstanceState: Bundle?): View? {
        binding = DataBindingUtil.inflate(inflater,
                R.layout.fragment_groceries, container, false)

        val factory = GroceriesViewModelFactory(token!!, cluster!!)
        val vm = ViewModelProviders.of(this, factory)
                .get(GroceriesViewModel::class.java)
        binding.viewModel = vm
        binding.executePendingBindings()

        binding.rvGroceries.layoutManager = LinearLayoutManager(this.context)

        binding.rvGroceries.adapter = groceryAdapter
        vm.groceries.observe(this, Observer { it?.let {
            groceryAdapter.replaceData(it)
        } })
        vm.loadClusters()

        /*binding.btnAdd.setOnClickListener {
            val dialog = AddClusterDialog()
            dialog.show(supportFragmentManager, "Add cluster")
        }*/

        return binding.root
    }

    // TODO: Rename method, update argument and hook method into UI event
    fun onButtonPressed(uri: Uri) {
        listener?.onFragmentInteraction(uri)
    }

    override fun onAttach(context: Context) {
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
    }

    /**
     * This interface must be implemented by activities that contain this
     * fragment to allow an interaction in this fragment to be communicated
     * to the activity and potentially other fragments contained in that
     * activity.
     *
     *
     * See the Android Training lesson [Communicating with Other Fragments]
     * (http://developer.android.com/training/basics/fragments/communicating.html)
     * for more information.
     */
    interface OnFragmentInteractionListener {
        // TODO: Update argument type and name
        fun onFragmentInteraction(uri: Uri)
    }

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
