import React, { useState, Component} from 'react';
import {IconContext} from 'react-icons';
import * as FAIcons from 'react-icons/fa';
import * as GrIcons from 'react-icons/gr';
import {Link} from 'react-router-dom';
import {NavBarData} from './NavBarData';
import '../../styles.css';
import './NavBar.css';

export default class NavBar extends Component {
    constructor(props){
        super(props);
        this.state = {
            sidebar: false
        };
    }

    setSidebar = function(value){
        console.log("Setting sidebar to: " + value);
        this.setState({sidebar: value});
    };
    showSidebar = ()=> this.setSidebar(!this.state.sidebar)

    render(){
        return(
            <>
            <div className="navbar">
                <Link to='#' className='menu-bars'>
                    {/* <IconContext.Provider value ={{ color: '#04BFAD'}}>
                        <FAIcons.FaBars onClick={this.showSidebar}/>
                    </IconContext.Provider> */}
                    <div className={this.state.sidebar ? 'menu-btn open' : 'menu-btn'} onClick={this.showSidebar}>
                        <div className="menu-btn-burger"></div>
                    </div>
                </Link>
            </div>
            <nav className={this.state.sidebar ? 'nav-menu active' : 'nav-menu'}>
                <ul className='nav-menu-items'>
                    {NavBarData.map((item, index)=>{
                        return (
                            <li key={index} className={item.class}>
                                <Link to={item.path}>
                                    {item.icon}
                                    <span>{item.title}</span>
                                </Link>
                            </li>
                        );
                    })}
                </ul>
            </nav>
            </>
        );
    }
} 