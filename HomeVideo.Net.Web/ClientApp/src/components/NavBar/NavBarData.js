//TODO: This will eventually get populated via libaries (think movies, tv. etc)
import React from 'react';
import {IconContext} from 'react-icons';
import * as FAIcons from 'react-icons/fa';
import * as GrIcons from 'react-icons/gr';
import * as IoIcons from 'react-icons/io';
import * as RiIcons from 'react-icons/ri';
import './NavBar.less';

export const NavBarData = [
    {
        title: 'Home',
        path: '/',
        icon: <GrIcons.GrHome />,
        class: 'nav-text'
    },
    {
        title: 'Movies',
        path: '/library/test-movie',
        icon: <RiIcons.RiMovie2Fill />,
        class: 'nav-text'
    },
    {
        title: 'Tv-Shows',
        path: '/library/test-tv',
        icon: <IoIcons.IoMdDesktop />,
        class: 'nav-text'
    },
    {
        title: 'Settings',
        path: '/settings',
        icon: <IoIcons.IoMdCog/>,
        class: 'nav-text'
    }
]