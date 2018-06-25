package com.groger.grogerapp.view.fragment

import android.arch.lifecycle.Observer
import android.arch.lifecycle.ViewModelProviders
import android.databinding.DataBindingUtil
import android.os.Bundle
import android.support.v4.app.Fragment
import android.support.v7.widget.LinearLayoutManager
import android.support.v7.widget.SearchView
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import com.groger.grogerapp.R
import com.groger.grogerapp.databinding.FragmentAddGroceryBinding
import com.groger.grogerapp.service.model.AddGrocery
import com.groger.grogerapp.service.model.Cluster
import com.groger.grogerapp.service.model.Grocery
import com.groger.grogerapp.service.model.NewGrocery
import com.groger.grogerapp.view.adapter.GroceryAdapter
import com.groger.grogerapp.view.listener.groceries.AddGroceryListener
import com.groger.grogerapp.view.listener.groceries.GroceriesInteractionListener
import com.groger.grogerapp.view.listener.groceries.NewGroceryListener
import com.groger.grogerapp.view.ui.AddExistingGroceryDialog
import com.groger.grogerapp.view.ui.AddGroceryDialog
import com.groger.grogerapp.viewmodel.GroceriesViewModel
import com.groger.grogerapp.viewmodel.factory.GroceriesViewModelFactory

private const val ARG_TOKEN = "token"
private const val ARG_CLUSTER = "cluster"

class AddGroceryFragment : Fragment(), GroceriesInteractionListener, NewGroceryListener {
    override fun onNewGrocery(grocery: NewGrocery) {
        viewModel.createGrocery(grocery)
        fragmentManager?.popBackStack()
    }

    private var token: String? = null
    lateinit var binding : FragmentAddGroceryBinding
    lateinit var viewModel : GroceriesViewModel
    private var cluster : Cluster? = null
    private val groceryAdapter = GroceryAdapter(arrayListOf(), this)

    override fun onRemoveGrocery(grocery: Grocery) { }

    override fun onGrocerySelected(grocery: Grocery) {
        val dialog = AddExistingGroceryDialog()
        dialog.grocery.name = grocery.name
        dialog.setAddExistingGroceryListener(object: AddGroceryListener{
            override fun onAddGrocery(addedGrocery: AddGrocery) {
                viewModel.addGrocery(grocery.id, addedGrocery)
                fragmentManager?.popBackStack()
            }
        })
        dialog.show(fragmentManager, "Add existing grocery")

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
                R.layout.fragment_add_grocery, container, false)

        val factory = GroceriesViewModelFactory(token!!, cluster!!)
        viewModel = ViewModelProviders.of(this, factory)
                .get(GroceriesViewModel::class.java)
        binding.executePendingBindings()

        binding.rvGroceries.layoutManager = LinearLayoutManager(this.context)

        binding.rvGroceries.adapter = groceryAdapter
        viewModel.groceries.observe(this, Observer { it?.let {
            groceryAdapter.replaceData(it)
        } })

        binding.btnNew.setOnClickListener {
            val dialog = AddGroceryDialog()
            dialog.setNewGroceryListener(this)
            dialog.show(fragmentManager, "Add grocery")
        }

        binding.svGrocery.setOnQueryTextListener(object : SearchView.OnQueryTextListener {
            override fun onQueryTextSubmit(query: String?): Boolean {
                if (query != null)
                    viewModel.loadGroceries(query)
                return true
            }

            override fun onQueryTextChange(newText: String?): Boolean {
                if (newText != null)
                    viewModel.loadGroceries(newText)
                return true
            }
        })

        return binding.root
    }

    /*override fun onAttach(context: Context) {
        super.onAttach(context)
        if (context is OnGroceryAddListener) {
            listener = context
        } else {
            throw RuntimeException(context.toString() + " must implement OnGroceryAddListener")
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
         * @param token token.
         * @return A new instance of fragment AddGroceryFragment.
         */
        @JvmStatic
        fun newInstance(token: String, cluster: Cluster) =
                AddGroceryFragment().apply {
                    arguments = Bundle().apply {
                        putString(ARG_TOKEN, token)
                        putSerializable(ARG_CLUSTER, cluster)
                    }
                }
    }
}
