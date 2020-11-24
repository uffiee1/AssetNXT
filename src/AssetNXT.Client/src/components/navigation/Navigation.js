import React, { useState } from 'react';
import {
  Collapse,
  Navbar,
  NavbarToggler,
  NavbarBrand,
  Nav,
  NavItem,
  NavLink,
  UncontrolledDropdown,
  DropdownToggle,
  DropdownMenu,
  DropdownItem,
  NavbarText
} from 'reactstrap';

const Navigation = (props) => {
  const [isOpen, setIsOpen] = useState(false);

  const toggle = () => setIsOpen(!isOpen);

  return (
    <div>
      <Navbar color="light" light expand="md">
        <NavbarBrand href="/">AssetNXT</NavbarBrand>
        <NavbarToggler onClick={toggle} />

            <Collapse isOpen={isOpen} navbar>
                <Nav className="mr-auto" fluid="lg" navbar>
                    <NavItem>
                        <NavLink href="/constrains/">Constrains</NavLink>
                    </NavItem>
                    <NavItem>
                        <NavLink href="/notifications/">Notifications</NavLink>
                    </NavItem>
                    <NavItem>
                        <NavLink href="/geo/">Geometrics</NavLink>
                    </NavItem>
                </Nav>
            </Collapse>
      </Navbar>
    </div>
  );
}

export default Navigation;